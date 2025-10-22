using Hospital_Business;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Hospital_System.Prescriptions
{
    public partial class frmAddPrescription : Form
    {
        private int _AppointmentID;
        private clsPrescription _Prescription;
        public frmAddPrescription(int AppointmentID)
        {
            InitializeComponent();
            _AppointmentID = AppointmentID;
            _Prescription = new clsPrescription(); 
        }

        private void btnClose_Click(object sender, EventArgs e) => this.Close();

        private void frmAddPrescription_Load(object sender, EventArgs e) =>
            lblAppointmentID.Text = _AppointmentID.ToString();

        public void txt_Validating(object sender, CancelEventArgs e) =>
            clsValidation.ValidateRequiredTextBox(sender, errorProvider1, e);

        private void _LoadData()
        {
            _Prescription.AppointmentID = _AppointmentID;
            _Prescription.MedicationName = txtMedicationName.Text.Trim();
            _Prescription.Dosage = txtDosage.Text.Trim();
            _Prescription.DurationDays = Convert.ToByte(nudDurationDays.Value);
            _Prescription.DurationMonths = Convert.ToByte(nudMonths.Value);
        }

        private void _HandleSave()
        {
            lblPrescriptionID.Text = _Prescription.PrescriptionID.ToString();
            txtMedicationName.Enabled = false;
            txtDosage.Enabled = false;
            nudDurationDays.Enabled = false;
            nudMonths.Enabled = false;
            btnSave.Enabled = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(!this.ValidateChildren())
            {
                MessageBox.Show("Please check red circle(s).", "Missing Info", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _LoadData();
            if(_Prescription.Save())
            {
                MessageBox.Show("Prescription is added successfully.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                _HandleSave();
            }
            else
                MessageBox.Show("An error occurred, and the prescription is not added.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
