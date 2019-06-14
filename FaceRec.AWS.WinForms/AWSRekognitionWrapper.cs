using Amazon.Rekognition;
using Amazon.Rekognition.Model;
using FaceRec.AWS.WinForms.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FaceRec.AWS.WinForms
{
    public static class AWSRekognitionWrapper
    {
        private static AmazonRekognitionClient _cachedClient;
        private static AmazonRekognitionClient CachedClient
        {
            get
            {
                if (_cachedClient == null)
                {
                    _cachedClient = GetClient();
                }

                return _cachedClient;
            }
        }

        private static DateTime DatLastRequest;
        private static TimeSpan MinIntervalBetweenRequests;

        public static bool IsProcessing { get; private set; }

        static AWSRekognitionWrapper()
        {
            DatLastRequest = DateTime.MinValue;
            MinIntervalBetweenRequests = TimeSpan.FromMilliseconds(Settings.Default.AmazonRekognitionIntevalBetweenRequests);
        }

        public static Rectangle[] DetectFaces(Bitmap picture)
        {
            using (MemoryStream ms = new MemoryStream())
            using (AmazonRekognitionClient client = GetClient())
            {
                picture.Save(ms, ImageFormat.Jpeg);

                DetectFacesResponse result = client.DetectFaces(new DetectFacesRequest
                {
                    Image = new Amazon.Rekognition.Model.Image
                    {
                        Bytes = ms
                    }
                });

                return result.FaceDetails
                    .Select(f => f.BoundingBox.ToRectangle(picture.Width, picture.Height))
                    .ToArray();
            }
        }

        public static async Task<string> AddFace(Bitmap singleFacePicture)
        {
            (Rectangle[] recognizedFaces, string[] faceIds, _) = await RecognizeFaces(singleFacePicture);
            // Rosto já existe na AWS
            if (recognizedFaces.Length > 0)
            {
                return faceIds[0];
            }

            using (MemoryStream ms = new MemoryStream())
            using (AmazonRekognitionClient client = GetClient())
            {
                singleFacePicture.Save(ms, ImageFormat.Jpeg);

                IndexFacesResponse response = client.IndexFaces(new IndexFacesRequest
                {
                    CollectionId = Settings.Default.AmazonRekognitionCollectionID,
                    Image = new Amazon.Rekognition.Model.Image
                    {
                        Bytes = ms
                    },
                });

                if (response.FaceRecords.Count == 0)
                {
                    return null;
                }

                return response.FaceRecords[0].Face.FaceId;
            }
        }

        private static async Task<SearchFacesByImageResponse> RecognizeSingleFace(Bitmap singleFacePicture)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                singleFacePicture.Save(ms, ImageFormat.Jpeg);
                try
                {
                    return await CachedClient.SearchFacesByImageAsync(new SearchFacesByImageRequest
                    {
                        CollectionId = Settings.Default.AmazonRekognitionCollectionID,
                        FaceMatchThreshold = 80,
                        Image = new Amazon.Rekognition.Model.Image
                        {
                            Bytes = ms
                        }
                    });
                }
                catch (InvalidParameterException)
                {
                    // Foto sem rosto
                    return null;
                }
            }
        }

        public static async Task<(Rectangle[] recognizedFaces, string[] faceIds, Rectangle[] unknownFaces)> RecognizeFaces(Bitmap picture)
        {
            IsProcessing = true;

            Rectangle[] allFacesInFrame = DetectFaces(picture);

            List<Rectangle> recognizedFaces = new List<Rectangle>();
            List<string> faceIds = new List<string>();
            List<Rectangle> unknownFaces = new List<Rectangle>();

            foreach (Rectangle faceRect in allFacesInFrame)
            {
                Bitmap faceCropPicture = picture.Clone(faceRect, picture.PixelFormat);
                SearchFacesByImageResponse response = await RecognizeSingleFace(faceCropPicture);

                // Não achei nenhum rosto nessa foto
                if (response == null || response.FaceMatches.Count == 0)
                {
                    unknownFaces.Add(faceRect);
                }
                else
                {
                    recognizedFaces.Add(faceRect);
                    faceIds.Add(response.FaceMatches.First().Face.FaceId);
                }

                await Task.Delay(GetSleepIntervalMillis());
            }

            IsProcessing = false;
            return (recognizedFaces.ToArray(), faceIds.ToArray(), unknownFaces.ToArray());
        }

        private static Rectangle ToRectangle(this BoundingBox boundingBox, int imgWidth, int imgHeight)
        {
            Rectangle rect = new Rectangle
            (
                Convert.ToInt32(Math.Round(imgWidth * boundingBox.Left)),
                Convert.ToInt32(Math.Round(imgHeight * boundingBox.Top)),
                Convert.ToInt32(Math.Round(imgWidth * boundingBox.Width)),
                Convert.ToInt32(Math.Round(imgHeight * boundingBox.Height))
            );

            if (rect.X + rect.Width > imgWidth)
            {
                rect.Width = imgWidth - rect.X;
            }

            if (rect.Y + rect.Height > imgHeight)
            {
                rect.Height = imgHeight - rect.Y;
            }

            return rect;
        }

        private static AmazonRekognitionClient GetClient()
        {
            return new AmazonRekognitionClient
            (
                Settings.Default.AmazonRekognitionAccessKeyID,
                Settings.Default.AmazonRekognitionSecretAccessKey,
                Amazon.RegionEndpoint.USEast2
            );
        }

        private static int GetSleepIntervalMillis()
        {
            DateTime now = DateTime.Now;
            try
            {
                if (now - DatLastRequest > MinIntervalBetweenRequests)
                {
                    return 0;
                }

                return Convert.ToInt32((MinIntervalBetweenRequests - (now - DatLastRequest)).TotalMilliseconds);
            }
            finally
            {
                DatLastRequest = now;
            }
        }
    }
}
