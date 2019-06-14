namespace FaceRec.AWS.WinForms
{
    partial class FrmAddPerson
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.picPerson = new System.Windows.Forms.PictureBox();
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.btnWebcamCapture = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnAddPerson = new System.Windows.Forms.Button();
            this.txtNomPerson = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picPerson)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // picPerson
            // 
            this.picPerson.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.tableLayoutPanel1.SetColumnSpan(this.picPerson, 4);
            this.picPerson.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picPerson.Location = new System.Drawing.Point(4, 4);
            this.picPerson.Margin = new System.Windows.Forms.Padding(4);
            this.picPerson.Name = "picPerson";
            this.picPerson.Size = new System.Drawing.Size(793, 437);
            this.picPerson.TabIndex = 0;
            this.picPerson.TabStop = false;
            // 
            // btnOpenFile
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.btnOpenFile, 2);
            this.btnOpenFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnOpenFile.Location = new System.Drawing.Point(4, 449);
            this.btnOpenFile.Margin = new System.Windows.Forms.Padding(4);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(392, 32);
            this.btnOpenFile.TabIndex = 3;
            this.btnOpenFile.Text = "Abrir de Arquivo";
            this.btnOpenFile.UseVisualStyleBackColor = true;
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // btnWebcamCapture
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.btnWebcamCapture, 2);
            this.btnWebcamCapture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnWebcamCapture.Location = new System.Drawing.Point(404, 449);
            this.btnWebcamCapture.Margin = new System.Windows.Forms.Padding(4);
            this.btnWebcamCapture.Name = "btnWebcamCapture";
            this.btnWebcamCapture.Size = new System.Drawing.Size(393, 32);
            this.btnWebcamCapture.TabIndex = 4;
            this.btnWebcamCapture.Text = "Capturar da Webcam";
            this.btnWebcamCapture.UseVisualStyleBackColor = true;
            this.btnWebcamCapture.Click += new System.EventHandler(this.btnWebcamCapture_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 151F));
            this.tableLayoutPanel1.Controls.Add(this.picPerson, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnAddPerson, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.btnOpenFile, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtNomPerson, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.btnWebcamCapture, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(801, 545);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // btnAddPerson
            // 
            this.btnAddPerson.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAddPerson.Location = new System.Drawing.Point(654, 509);
            this.btnAddPerson.Margin = new System.Windows.Forms.Padding(4);
            this.btnAddPerson.Name = "btnAddPerson";
            this.btnAddPerson.Size = new System.Drawing.Size(143, 32);
            this.btnAddPerson.TabIndex = 5;
            this.btnAddPerson.Text = "Adicionar";
            this.btnAddPerson.UseVisualStyleBackColor = true;
            this.btnAddPerson.Click += new System.EventHandler(this.btnAddPerson_Click);
            // 
            // txtNomPerson
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.txtNomPerson, 2);
            this.txtNomPerson.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtNomPerson.Location = new System.Drawing.Point(154, 513);
            this.txtNomPerson.Margin = new System.Windows.Forms.Padding(4, 8, 4, 4);
            this.txtNomPerson.Name = "txtNomPerson";
            this.txtNomPerson.Size = new System.Drawing.Size(492, 25);
            this.txtNomPerson.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(5, 510);
            this.label1.Margin = new System.Windows.Forms.Padding(5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(140, 30);
            this.label1.TabIndex = 1;
            this.label1.Text = "Nome da Pessoa:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmAddPerson
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(801, 545);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmAddPerson";
            this.Text = "Adicionar Pessoa";
            this.Load += new System.EventHandler(this.FrmAddPerson_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picPerson)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picPerson;
        private System.Windows.Forms.Button btnOpenFile;
        private System.Windows.Forms.Button btnWebcamCapture;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnAddPerson;
        private System.Windows.Forms.TextBox txtNomPerson;
        private System.Windows.Forms.Label label1;
    }
}