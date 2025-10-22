namespace Hospital_System.Prescriptions
{
    partial class frmPrescriptionInfo
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
            this.ctrlPrescriptionInfo1 = new Hospital_System.Prescription.ctrlPrescriptionInfo();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ctrlPrescriptionInfo1
            // 
            this.ctrlPrescriptionInfo1.Location = new System.Drawing.Point(-4, 0);
            this.ctrlPrescriptionInfo1.Name = "ctrlPrescriptionInfo1";
            this.ctrlPrescriptionInfo1.Size = new System.Drawing.Size(919, 288);
            this.ctrlPrescriptionInfo1.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Location = new System.Drawing.Point(793, 294);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(109, 23);
            this.btnClose.TabIndex = 14;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmPrescriptionInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(914, 322);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.ctrlPrescriptionInfo1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmPrescriptionInfo";
            this.Text = "Prescription Info";
            this.Load += new System.EventHandler(this.frmPrescriptionInfo_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Prescription.ctrlPrescriptionInfo ctrlPrescriptionInfo1;
        private System.Windows.Forms.Button btnClose;
    }
}