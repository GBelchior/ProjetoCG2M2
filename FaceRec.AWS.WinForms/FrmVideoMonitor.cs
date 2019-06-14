using AForge.Video;
using System;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace FaceRec.AWS.WinForms
{
    public partial class FrmVideoMonitor : Form
    {
        private DateTime lastBeep;
        private IVideoSource VideoSource;
        private FaceRecognizer FaceRecognizer;

        public FrmVideoMonitor(IVideoSource videoSource)
        {
            InitializeComponent();

            lastBeep = DateTime.MinValue;
            VideoSource = videoSource;

            FaceRecognizer = new FaceRecognizer(VideoSource);
            FaceRecognizer.NewFrame += NewFrame;
        }

        public void Start()
        {
            FaceRecognizer.Start();
        }

        public void Stop()
        {
            FaceRecognizer.Stop();
        }

        private void NewFrame(object sender, FaceRecognizerEventArgs e)
        {
            if (IsDisposed || !IsHandleCreated) return;

            Invoke(new Action(() =>
            {
                Image oldPicture = picMonitor.BackgroundImage;
                picMonitor.BackgroundImage = e.Frame;
                oldPicture?.Dispose();

                DateTime now = DateTime.Now;
                if (!ContainsFocus && e.ContainsUnknownFaces && now > lastBeep.AddSeconds(10))
                {
                    Activate();
                    SystemSounds.Beep.Play();

                    lastBeep = now;
                }
            }));
        }

        private void FrmVideoMonitor_FormClosing(object sender, FormClosingEventArgs e)
        {
            Stop();
        }
    }
}
