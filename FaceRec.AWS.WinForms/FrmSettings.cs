using FaceRec.AWS.WinForms.Properties;
using System;
using System.Windows.Forms;

namespace FaceRec.AWS.WinForms
{
    public partial class FrmSettings : Form
    {
        public FrmSettings()
        {
            InitializeComponent();
        }

        private void FrmSettings_Load(object sender, EventArgs e)
        {
            txtAccessKeyID.Text = Settings.Default.AmazonRekognitionAccessKeyID;
            txtSecretAccessKey.Text = Settings.Default.AmazonRekognitionSecretAccessKey;
            txtCollectionID.Text = Settings.Default.AmazonRekognitionCollectionID;
            nudMaxRequestsPerSecond.Value = Settings.Default.AmazonRekognitionIntevalBetweenRequests;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Settings.Default.AmazonRekognitionAccessKeyID = txtAccessKeyID.Text.Trim();
            Settings.Default.AmazonRekognitionSecretAccessKey = txtSecretAccessKey.Text.Trim();
            Settings.Default.AmazonRekognitionCollectionID = txtCollectionID.Text.Trim();
            Settings.Default.AmazonRekognitionIntevalBetweenRequests = Convert.ToInt32(nudMaxRequestsPerSecond.Value);

            Settings.Default.Save();
            Close();
        }
    }
}
