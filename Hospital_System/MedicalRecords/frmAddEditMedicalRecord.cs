using Hospital_Business;
using Hospital_System.Prescriptions;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Hospital_System.MedicalRecords
{
    public partial class frmAddEditMedicalRecord : Form
    {
        private clsMedicalRecord _MedicalRecord;
        private int _AppointmentID;

        public frmAddEditMedicalRecord(int AppointmentID)
        {
            InitializeComponent();
            _MedicalRecord = new clsMedicalRecord();
            _AppointmentID = AppointmentID;
        }

        private void btnClose_Click(object sender, EventArgs e) => this.Close();
        
        private void _LoadDataToMedicalRecord()
        {
            _MedicalRecord.AppointmentID = _AppointmentID;
            _MedicalRecord.Symptoms = txtSymptoms.Text.Trim();
            _MedicalRecord.Diagnosis = txtDiagnosis.Text.Trim();
            _MedicalRecord.MedicalRecordNotes = txtNotes.Text.Trim();
        }

        private void _HandleSave()
        {
            _LoadDataToMedicalRecord();
            if (_MedicalRecord.Save())
            {
                MessageBox.Show("Medical record is created successfuly", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblMedicalRecordID.Text = _MedicalRecord.MedicalRecordID.ToString();
                txtSymptoms.Enabled = false;
                txtDiagnosis.Enabled = false;
                txtNotes.Enabled = false;
            }
            else
                MessageBox.Show("An error occured during record creation", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(!this.ValidateChildren())
            {
                MessageBox.Show("Please check red circle(s).", "Missing Details",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("Are you sure you want to create this medical record?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;

            _HandleSave();
        }

        private void llAddPrescription_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Check red circle(s)", "Missing Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            frmAddPrescription frm = new frmAddPrescription(_AppointmentID);
            frm.ShowDialog();
        }

        private void frmAddEditMedicalRecord_Load(object sender, EventArgs e) =>
            lblAppointmentID.Text = _AppointmentID.ToString();

        private void txtDiagnosis_Validating(object sender, CancelEventArgs e) =>
            clsValidation.ValidateRequiredTextBox(sender, errorProvider1, e);
    }
}
