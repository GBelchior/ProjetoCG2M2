using Amazon.Rekognition;
using Amazon.Rekognition.Model;
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
        private static AmazonRekognitionClient CachedClient;
        private static DateTime DatLastRequest;
        private static TimeSpan MinIntervalBetweenRequests;

        public static bool IsWaiting { get; private set; }

        static AWSRekognitionWrapper()
        {
            DatLastRequest = DateTime.MinValue;
            MinIntervalBetweenRequests = TimeSpan.FromMilliseconds(Properties.Settings.Default.AmazonRekognitionIntevalBetweenRequests);
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

        public static string AddFace(Bitmap picture)
        {
            using (MemoryStream ms = new MemoryStream())
            using (AmazonRekognitionClient client = GetClient())
            {
                picture.Save(ms, ImageFormat.Jpeg);

                IndexFacesResponse response = client.IndexFaces(new IndexFacesRequest
                {
                    CollectionId = Properties.Settings.Default.AmazonRekognitionCollectionID,
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

        // TODO:
        // You can also call the DetectFaces operation and use the bounding boxes in the response to make face crops, 
        // which then you can pass in to the SearchFacesByImage operation.
        // https://docs.aws.amazon.com/rekognition/latest/dg/API_SearchFacesByImage.html
        // REVISAR ESSE MÉTODO
        // O SEARCHFACES RETORNA UM VALOR PARA TODOS OS ROSTOS DA COLEÇÃO
        // FAZER O SEGUINTE:
        // MANDAR UM ROSTO POR VEZ PRO SERVIÇO
        // COLOCA O FACEMATCH = 80
        // AI SE NAO VIER RESULTADO MANDA BALA
        public static async Task<(Rectangle[] recognizedFaces, string[] faceIds, Rectangle[] unknownFaces)> RecognizeFaces(Bitmap picture)
        {
            IsWaiting = true;
            await Task.Delay(GetSleepIntervalMillis());

            if (CachedClient == null)
            {
                CachedClient = GetClient();
            }

            using (MemoryStream ms = new MemoryStream())
            {
                picture.Save(ms, ImageFormat.Jpeg);
                SearchFacesByImageResponse response = null;
                try
                {
                    response = await CachedClient.SearchFacesByImageAsync(new SearchFacesByImageRequest
                    {
                        CollectionId = Properties.Settings.Default.AmazonRekognitionCollectionID,
                        FaceMatchThreshold = 0,
                        Image = new Amazon.Rekognition.Model.Image
                        {
                            Bytes = ms
                        }
                    });
                }
                catch (InvalidParameterException)
                {
                    // Foto sem rosto
                    IsWaiting = false;
                    return (new Rectangle[0], new string[0], new Rectangle[0]);
                }

                List<FaceMatch> recognizedFaces = response.FaceMatches.Where(f => f.Similarity >= 80).ToList();
                List<FaceMatch> unknownFaces = response.FaceMatches.Where(f => f.Similarity < 80).ToList();

                Rectangle[] recognizedFacePositions = recognizedFaces.Select(f => f.Face.BoundingBox.ToRectangle(picture.Width, picture.Height)).ToArray();
                string[] faceIds = recognizedFaces.Select(f => f.Face.FaceId).ToArray();
                Rectangle[] unknownFacePositions = unknownFaces.Select(f => f.Face.BoundingBox.ToRectangle(picture.Width, picture.Height)).ToArray();

                IsWaiting = false;
                return (recognizedFacePositions, faceIds, unknownFacePositions);
            }
        }

        private static Rectangle ToRectangle(this BoundingBox boundingBox, int imgWidth, int imgHeight)
        {
            return new Rectangle
            (
                Convert.ToInt32(Math.Round(imgWidth * boundingBox.Left)),
                Convert.ToInt32(Math.Round(imgHeight * boundingBox.Top)),
                Convert.ToInt32(Math.Round(imgWidth * boundingBox.Width)),
                Convert.ToInt32(Math.Round(imgHeight * boundingBox.Height))
            );
        }

        private static AmazonRekognitionClient GetClient()
        {
            return new AmazonRekognitionClient
            (
                Properties.Settings.Default.AmazonRekognitionAccessKeyID,
                Properties.Settings.Default.AmazonRekognitionSecretAccessKey,
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
