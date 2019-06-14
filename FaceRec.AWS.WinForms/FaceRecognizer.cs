using AForge.Video;
using System;
using System.Drawing;

namespace FaceRec.AWS.WinForms
{
    public class FaceRecognizer
    {
        public event EventHandler<FaceRecognizerEventArgs> NewFrame;

        private Pen okPen;
        private Pen unknownPen;
        private Font font;
        private IVideoSource VideoSource;

        public FaceRecognizer(IVideoSource videoSource)
        {
            VideoSource = videoSource;

            VideoSource.NewFrame += VideoSource_NewFrame;

            font = new Font("Segoe UI", 14);
            okPen = new Pen(Brushes.Green, 5);
            unknownPen = new Pen(Brushes.Red, 5);
        }

        public void Start()
        {
            VideoSource.Start();
        }

        public void Stop()
        {
            VideoSource.Stop();
        }

        private async void VideoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            if (AWSRekognitionWrapper.IsWaiting) return;

            Bitmap newBitmap = new Bitmap(eventArgs.Frame);
            eventArgs.Frame.Dispose();

            (Rectangle[] recognizedFaces, string[] faceIds, Rectangle[] unknownFaces) =
                await AWSRekognitionWrapper.RecognizeFaces(newBitmap);

            using (Graphics g = Graphics.FromImage(newBitmap))
            {
                for (int i = 0; i < recognizedFaces.Length; i++)
                {
                    Rectangle rect = recognizedFaces[i];
                    string faceName = FaceDatabase.GetFaceName(faceIds[i]);

                    g.DrawRectangle(okPen, rect);
                    g.DrawString(faceName, font, okPen.Brush, rect.Left, rect.Bottom + 10);
                }

                foreach (Rectangle rectUnknown in unknownFaces)
                {
                    g.DrawRectangle(unknownPen, rectUnknown);
                }
            }

            NewFrame?.Invoke(this, new FaceRecognizerEventArgs(newBitmap, unknownFaces.Length > 0));
        }
    }

    public class FaceRecognizerEventArgs : EventArgs
    {
        public Bitmap Frame { get; set; }
        public bool ContainsUnknownFaces { get; set; }

        public FaceRecognizerEventArgs(Bitmap frame, bool containsUnknownFaces)
        {
            Frame = frame;
            ContainsUnknownFaces = containsUnknownFaces;
        }
    }
}
