namespace Hospital_System.Appointments
{
    partial class frmAppointmentInfo
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
            this.ctrlAppointmentInfo1 = new Hospital_System.Appointments.ctrlAppointmentInfo();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ctrlAppointmentInfo1
            // 
            this.ctrlAppointmentInfo1.Location = new System.Drawing.Point(-1, -1);
            this.ctrlAppointmentInfo1.Name = "ctrlAppointmentInfo1";
            this.ctrlAppointmentInfo1.Size = new System.Drawing.Size(945, 346);
            this.ctrlAppointmentInfo1.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Location = new System.Drawing.Point(835, 351);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(109, 23);
            this.btnClose.TabIndex = 13;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmAppointmentInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(952, 383);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.ctrlAppointmentInfo1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "frmAppointmentInfo";
            this.Text = "Appointment Info";
            this.Load += new System.EventHandler(this.frmAppointmentInfo_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ctrlAppointmentInfo ctrlAppointmentInfo1;
        private System.Windows.Forms.Button btnClose;
    }
}