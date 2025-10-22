namespace Hospital_System.Patients
{
    partial class frmPatientHistory
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
            this.ctrlPatientInfo1 = new Hospital_System.Patients.ctrlPatientInfo();
            this.dgvHistoryRecords = new System.Windows.Forms.DataGridView();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistoryRecords)).BeginInit();
            this.SuspendLayout();
            // 
            // ctrlPatientInfo1
            // 
            this.ctrlPatientInfo1.Location = new System.Drawing.Point(-5, -7);
            this.ctrlPatientInfo1.Name = "ctrlPatientInfo1";
            this.ctrlPatientInfo1.Size = new System.Drawing.Size(876, 336);
            this.ctrlPatientInfo1.TabIndex = 0;
            // 
            // dgvHistoryRecords
            // 
            this.dgvHistoryRecords.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHistoryRecords.Location = new System.Drawing.Point(12, 354);
            this.dgvHistoryRecords.Name = "dgvHistoryRecords";
            this.dgvHistoryRecords.Size = new System.Drawing.Size(848, 171);
            this.dgvHistoryRecords.TabIndex = 1;
            // 
            // btnClose
            // 
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Location = new System.Drawing.Point(748, 538);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(112, 23);
            this.btnClose.TabIndex = 62;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmPatientHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(865, 573);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dgvHistoryRecords);
            this.Controls.Add(this.ctrlPatientInfo1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmPatientHistory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmPatientMedicalRecordsHistory";
            this.Load += new System.EventHandler(this.frmPatientHistory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistoryRecords)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ctrlPatientInfo ctrlPatientInfo1;
        private System.Windows.Forms.DataGridView dgvHistoryRecords;
        private System.Windows.Forms.Button btnClose;
    }
}