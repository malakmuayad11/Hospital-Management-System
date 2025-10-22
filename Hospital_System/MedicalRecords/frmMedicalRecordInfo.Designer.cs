namespace Hospital_System.MedicalRecords
{
    partial class frmMedicalRecordInfo
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
            this.ctrlMedicalRecordInfo1 = new Hospital_System.MedicalRecords.ctrlMedicalRecordInfo();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ctrlMedicalRecordInfo1
            // 
            this.ctrlMedicalRecordInfo1.Dock = System.Windows.Forms.DockStyle.Left;
            this.ctrlMedicalRecordInfo1.Location = new System.Drawing.Point(0, 0);
            this.ctrlMedicalRecordInfo1.Name = "ctrlMedicalRecordInfo1";
            this.ctrlMedicalRecordInfo1.Size = new System.Drawing.Size(783, 324);
            this.ctrlMedicalRecordInfo1.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Location = new System.Drawing.Point(697, 291);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 48;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmMedicalRecordInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 324);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.ctrlMedicalRecordInfo1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmMedicalRecordInfo";
            this.Text = "Medical Record Info";
            this.Load += new System.EventHandler(this.frmMedicalRecordInfo_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ctrlMedicalRecordInfo ctrlMedicalRecordInfo1;
        private System.Windows.Forms.Button btnClose;
    }
}