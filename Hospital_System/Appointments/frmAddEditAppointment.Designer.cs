namespace Hospital_System.Appointments
{
    partial class frmAddEditAppointment
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAddEditAppointment));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpPatientInfo = new System.Windows.Forms.TabPage();
            this.ctrlPatientInfoWithFilter1 = new Hospital_System.Patients.ctrlPatientInfoWithFilter();
            this.tpDoctorInfo = new System.Windows.Forms.TabPage();
            this.ctrlDoctorInfoWithFilter1 = new Hospital_System.Doctors.ctrlDoctorInfoWithFilter();
            this.tpAppointmentInfo = new System.Windows.Forms.TabPage();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.dtpAppointmentTime = new System.Windows.Forms.DateTimePicker();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtReasonForVisit = new System.Windows.Forms.TextBox();
            this.dtpAppointmentDate = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblAppointmentID = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.tabControl1.SuspendLayout();
            this.tpPatientInfo.SuspendLayout();
            this.tpDoctorInfo.SuspendLayout();
            this.tpAppointmentInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpPatientInfo);
            this.tabControl1.Controls.Add(this.tpDoctorInfo);
            this.tabControl1.Controls.Add(this.tpAppointmentInfo);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1032, 605);
            this.tabControl1.TabIndex = 0;
            // 
            // tpPatientInfo
            // 
            this.tpPatientInfo.Controls.Add(this.ctrlPatientInfoWithFilter1);
            this.tpPatientInfo.Location = new System.Drawing.Point(4, 22);
            this.tpPatientInfo.Name = "tpPatientInfo";
            this.tpPatientInfo.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tpPatientInfo.Size = new System.Drawing.Size(1020, 527);
            this.tpPatientInfo.TabIndex = 0;
            this.tpPatientInfo.Text = "Patient Info";
            this.tpPatientInfo.UseVisualStyleBackColor = true;
            // 
            // ctrlPatientInfoWithFilter1
            // 
            this.ctrlPatientInfoWithFilter1.Location = new System.Drawing.Point(17, 0);
            this.ctrlPatientInfoWithFilter1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ctrlPatientInfoWithFilter1.Name = "ctrlPatientInfoWithFilter1";
            this.ctrlPatientInfoWithFilter1.Size = new System.Drawing.Size(941, 497);
            this.ctrlPatientInfoWithFilter1.TabIndex = 0;
            this.ctrlPatientInfoWithFilter1.FindPatient += new System.EventHandler<bool>(this.ctrlPatientInfoWithFilter1_FindPatient);
            // 
            // tpDoctorInfo
            // 
            this.tpDoctorInfo.Controls.Add(this.ctrlDoctorInfoWithFilter1);
            this.tpDoctorInfo.Location = new System.Drawing.Point(4, 22);
            this.tpDoctorInfo.Name = "tpDoctorInfo";
            this.tpDoctorInfo.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tpDoctorInfo.Size = new System.Drawing.Size(947, 562);
            this.tpDoctorInfo.TabIndex = 1;
            this.tpDoctorInfo.Text = "Doctor Information";
            this.tpDoctorInfo.UseVisualStyleBackColor = true;
            // 
            // ctrlDoctorInfoWithFilter1
            // 
            this.ctrlDoctorInfoWithFilter1.Location = new System.Drawing.Point(6, 6);
            this.ctrlDoctorInfoWithFilter1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ctrlDoctorInfoWithFilter1.Name = "ctrlDoctorInfoWithFilter1";
            this.ctrlDoctorInfoWithFilter1.Size = new System.Drawing.Size(933, 530);
            this.ctrlDoctorInfoWithFilter1.TabIndex = 0;
            this.ctrlDoctorInfoWithFilter1.FindDoctor += new System.EventHandler<bool>(this.ctrlDoctorInfoWithFilter1_FindDoctor);
            // 
            // tpAppointmentInfo
            // 
            this.tpAppointmentInfo.Controls.Add(this.pictureBox4);
            this.tpAppointmentInfo.Controls.Add(this.pictureBox3);
            this.tpAppointmentInfo.Controls.Add(this.pictureBox2);
            this.tpAppointmentInfo.Controls.Add(this.pictureBox1);
            this.tpAppointmentInfo.Controls.Add(this.pictureBox5);
            this.tpAppointmentInfo.Controls.Add(this.dtpAppointmentTime);
            this.tpAppointmentInfo.Controls.Add(this.lblStatus);
            this.tpAppointmentInfo.Controls.Add(this.btnClose);
            this.tpAppointmentInfo.Controls.Add(this.btnSave);
            this.tpAppointmentInfo.Controls.Add(this.txtReasonForVisit);
            this.tpAppointmentInfo.Controls.Add(this.dtpAppointmentDate);
            this.tpAppointmentInfo.Controls.Add(this.label5);
            this.tpAppointmentInfo.Controls.Add(this.label1);
            this.tpAppointmentInfo.Controls.Add(this.label4);
            this.tpAppointmentInfo.Controls.Add(this.label3);
            this.tpAppointmentInfo.Controls.Add(this.lblAppointmentID);
            this.tpAppointmentInfo.Controls.Add(this.label2);
            this.tpAppointmentInfo.Location = new System.Drawing.Point(4, 22);
            this.tpAppointmentInfo.Name = "tpAppointmentInfo";
            this.tpAppointmentInfo.Size = new System.Drawing.Size(1024, 579);
            this.tpAppointmentInfo.TabIndex = 2;
            this.tpAppointmentInfo.Text = "Appointment Information";
            this.tpAppointmentInfo.UseVisualStyleBackColor = true;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(252, 50);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(47, 37);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 51;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(252, 133);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(47, 37);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 50;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(252, 364);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(47, 37);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 49;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(252, 287);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(47, 37);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 48;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox5.Image")));
            this.pictureBox5.Location = new System.Drawing.Point(252, 211);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(47, 37);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox5.TabIndex = 47;
            this.pictureBox5.TabStop = false;
            // 
            // dtpAppointmentTime
            // 
            this.dtpAppointmentTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpAppointmentTime.Location = new System.Drawing.Point(335, 211);
            this.dtpAppointmentTime.Name = "dtpAppointmentTime";
            this.dtpAppointmentTime.ShowUpDown = true;
            this.dtpAppointmentTime.Size = new System.Drawing.Size(200, 20);
            this.dtpAppointmentTime.TabIndex = 46;
            this.dtpAppointmentTime.Validating += new System.ComponentModel.CancelEventHandler(this.dtpAppointmentTime_Validating);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Nirmala Text", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.Color.Red;
            this.lblStatus.Location = new System.Drawing.Point(329, 369);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(56, 32);
            this.lblStatus.TabIndex = 45;
            this.lblStatus.Text = "N/A";
            // 
            // btnClose
            // 
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Location = new System.Drawing.Point(776, 518);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 43;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Location = new System.Drawing.Point(858, 518);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 44;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtReasonForVisit
            // 
            this.txtReasonForVisit.Location = new System.Drawing.Point(335, 303);
            this.txtReasonForVisit.Name = "txtReasonForVisit";
            this.txtReasonForVisit.Size = new System.Drawing.Size(200, 20);
            this.txtReasonForVisit.TabIndex = 42;
            this.txtReasonForVisit.Validating += new System.ComponentModel.CancelEventHandler(this.txtReasonForVisit_Validating);
            // 
            // dtpAppointmentDate
            // 
            this.dtpAppointmentDate.CustomFormat = "DD/MM/YY";
            this.dtpAppointmentDate.Location = new System.Drawing.Point(335, 142);
            this.dtpAppointmentDate.Name = "dtpAppointmentDate";
            this.dtpAppointmentDate.Size = new System.Drawing.Size(205, 20);
            this.dtpAppointmentDate.TabIndex = 40;
            this.dtpAppointmentDate.Validating += new System.ComponentModel.CancelEventHandler(this.dtpAppointmentDate_Validating);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Nirmala Text", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(26, 369);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 32);
            this.label5.TabIndex = 22;
            this.label5.Text = "Status:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Nirmala Text", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(26, 292);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(184, 32);
            this.label1.TabIndex = 21;
            this.label1.Text = "Reason for Visit:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Nirmala Text", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(26, 211);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(220, 32);
            this.label4.TabIndex = 19;
            this.label4.Text = "Appointment Time:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Nirmala Text", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(26, 133);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(217, 32);
            this.label3.TabIndex = 17;
            this.label3.Text = "Appointment Date:";
            // 
            // lblAppointmentID
            // 
            this.lblAppointmentID.AutoSize = true;
            this.lblAppointmentID.Font = new System.Drawing.Font("Nirmala Text", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAppointmentID.ForeColor = System.Drawing.Color.Red;
            this.lblAppointmentID.Location = new System.Drawing.Point(329, 50);
            this.lblAppointmentID.Name = "lblAppointmentID";
            this.lblAppointmentID.Size = new System.Drawing.Size(56, 32);
            this.lblAppointmentID.TabIndex = 16;
            this.lblAppointmentID.Text = "N/A";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Nirmala Text", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(26, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(190, 32);
            this.label2.TabIndex = 15;
            this.label2.Text = "Appointment ID:";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // frmAddEditAppointment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1032, 605);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmAddEditAppointment";
            this.Text = "frmAddEditAppointment";
            this.Load += new System.EventHandler(this.frmAddEditAppointment_Load);
            this.tabControl1.ResumeLayout(false);
            this.tpPatientInfo.ResumeLayout(false);
            this.tpDoctorInfo.ResumeLayout(false);
            this.tpAppointmentInfo.ResumeLayout(false);
            this.tpAppointmentInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpPatientInfo;
        private System.Windows.Forms.TabPage tpDoctorInfo;
        private System.Windows.Forms.TabPage tpAppointmentInfo;
        private Patients.ctrlPatientInfoWithFilter ctrlPatientInfoWithFilter1;
        private Doctors.ctrlDoctorInfoWithFilter ctrlDoctorInfoWithFilter1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblAppointmentID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpAppointmentDate;
        private System.Windows.Forms.TextBox txtReasonForVisit;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.DateTimePicker dtpAppointmentTime;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}