using AForge.Video;
using AForge.Video.DirectShow;
using System;
using System.Windows.Forms;

namespace FaceRec.AWS.WinForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Size = new System.Drawing.Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            ShowForm<FrmAddPerson>();
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            ShowForm<FrmSettings>();
        }

        private void btnPhysicalCam_Click(object sender, EventArgs e)
        {
            FrmVideoDeviceSelector frm = new FrmVideoDeviceSelector();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                VideoCaptureDevice device = new VideoCaptureDevice(frm.SelectedDeviceMonikerString);
                device.VideoSourceError += (ss, ee) =>
                {
                    MessageBox.Show(ee.Description, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                };
                ShowMonitor(device);
            }
        }

        private void btnIPCam_Click(object sender, EventArgs e)
        {
            FrmMJPEGDeviceSelector frm = new FrmMJPEGDeviceSelector();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                MJPEGStream device = new MJPEGStream(frm.MJPEGCameraAddress.TrimEnd('/'));
                device.VideoSourceError += (ss, ee) =>
                {
                    MessageBox.Show(ee.Description, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                };
                ShowMonitor(device);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ShowForm<T>() where T : Form, new()
        {
            T form = new T();
            form.MdiParent = this;

            form.Show();
            form.Location = new System.Drawing.Point(Width / 2 - form.Width / 2, Height / 2 - form.Height / 2);
        }

        private void ShowMonitor(IVideoSource videoSource)
        {
            FrmVideoMonitor frmMonitor = new FrmVideoMonitor(videoSource);
            frmMonitor.MdiParent = this;
            frmMonitor.StartPosition = FormStartPosition.CenterParent;
            frmMonitor.Show();
            frmMonitor.Location = new System.Drawing.Point(Width / 2 - frmMonitor.Width / 2, Height / 2 - frmMonitor.Height / 2);

            frmMonitor.Start();
        }
    }
}
