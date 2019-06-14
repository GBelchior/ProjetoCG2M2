using AForge.Video.DirectShow;
using System;
using System.Windows.Forms;

namespace FaceRec.AWS.WinForms
{
    public partial class FrmVideoDeviceSelector : Form
    {
        public string SelectedDeviceMonikerString;

        private FilterInfoCollection VideoDevices;
        public FrmVideoDeviceSelector()
        {
            InitializeComponent();
        }

        private void FrmVideoDeviceSelector_Load(object sender, EventArgs e)
        {
            VideoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            cmbVideoDevices.DisplayMember = "Name";
            cmbVideoDevices.ValueMember = "MonikerString";
            cmbVideoDevices.DataSource = VideoDevices;
        }

        private void btnSelecionar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            SelectedDeviceMonikerString = cmbVideoDevices.SelectedValue.ToString();
            Close();
        }
    }
}
