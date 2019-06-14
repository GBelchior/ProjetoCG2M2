using AForge.Video.DirectShow;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace FaceRec.AWS.WinForms
{
    public partial class FrmAddPerson : Form
    {
        private OpenFileDialog OpenFileDialog;
        private Bitmap CurrentPicture;
        private VideoCaptureDevice WebCam;

        public FrmAddPerson()
        {
            InitializeComponent();
        }

        private void FrmAddPerson_Load(object sender, EventArgs e)
        {
            OpenFileDialog = new OpenFileDialog
            {
                AddExtension = true,
                Multiselect = false,
                Filter = "Arquivos de imagem (*.jpg;*.jpeg;*.jpe;*.jfif;*.png;*.bmp)|*.jpg;*.jpeg;*.jpe;*.jfif;*.png;*.bmp"
            };
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            if (OpenFileDialog.ShowDialog() != DialogResult.OK) return;
            ChangePictureBox(new Bitmap(OpenFileDialog.FileName));
        }

        private void btnWebcamCapture_Click(object sender, EventArgs e)
        {
            if (WebCam == null)
            {
                LoadWebCam();

                if (WebCam == null)
                {
                    MessageBox.Show("Nenhuma Webcam selecionada", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            WebCam.NewFrame += WebCam_NewFrame;
            WebCam.Start();
        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            if (CurrentPicture == null)
            {
                MessageBox.Show("Nenhuma foto definida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Rectangle[] allFaces = AWSRekognitionWrapper.DetectFaces(CurrentPicture);

            Bitmap beforeRectangles = new Bitmap(CurrentPicture);
            Bitmap facesDetected = new Bitmap(CurrentPicture);
            Pen p = new Pen(Brushes.Green, 5);
            using (Graphics g = Graphics.FromImage(facesDetected))
            {
                foreach (Rectangle rect in allFaces)
                {
                    g.DrawRectangle(p, rect);
                }
            }

            ChangePictureBox(facesDetected);

            if (allFaces.Length == 0)
            {
                MessageBox.Show("Nenhum rosto encontrado nesta foto", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (allFaces.Length > 1)
            {
                MessageBox.Show("Foi encontrado mais de um rosto nesta foto. Use fotos com apenas um rosto.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (allFaces.Length == 1)
            {
                AWSRekognitionWrapper.AddFace(beforeRectangles);
                Close();
            }
        }

        private void LoadWebCam()
        {
            FrmVideoDeviceSelector frmVideoDeviceSelector = new FrmVideoDeviceSelector();
            if (frmVideoDeviceSelector.ShowDialog() == DialogResult.OK)
            {
                WebCam = new VideoCaptureDevice(frmVideoDeviceSelector.SelectedDeviceMonikerString);
            }
        }

        private void WebCam_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {
            Invoke(new Action(() =>
            {
                WebCam.NewFrame -= WebCam_NewFrame;

                ChangePictureBox(new Bitmap(eventArgs.Frame));

                WebCam.SignalToStop();
            }));
        }

        private void ChangePictureBox(Bitmap newPicture)
        {
            picPerson.BackgroundImage = null;
            CurrentPicture?.Dispose();

            CurrentPicture = newPicture;
            picPerson.BackgroundImage = CurrentPicture;
        }
    }
}
