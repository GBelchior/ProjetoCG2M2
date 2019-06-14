using System;
using System.Windows.Forms;

namespace FaceRec.AWS.WinForms
{
    public partial class FrmMJPEGDeviceSelector : Form
    {
        public string MJPEGCameraAddress { get; private set; }

        public FrmMJPEGDeviceSelector()
        {
            InitializeComponent();
        }

        private void btnSelecionar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            MJPEGCameraAddress = txtMJPEGAddress.Text;
            Close();
        }
    }
}
