using Hospital_Business;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Hospital_System.Patients
{
    public partial class frmAddEditPatient : Form
    {
        private clsPatient _Patient;
        private int _PatientID;
        private string _CurrentNationalNo;
        private enum enMode { AddNew = 1, Update = 2}
        private enMode _Mode;

        public event EventHandler<clsPatient> PatientAdded;

        public void OnPatientAdd(clsPatient Patient) =>
            PatientAdded?.Invoke(this, Patient);
           
        public frmAddEditPatient()
        {
            InitializeComponent();
            _Patient = new clsPatient();
            this._Mode = enMode.AddNew;
        }

        public frmAddEditPatient(int PatientID)
        {
            InitializeComponent();
            _PatientID = PatientID;
            _Patient = clsPatient.Find(PatientID);
            this._Mode= enMode.Update;
        }

        private void _LoadAddNewMode()
        {
            this.Text = "Add New Patient";
            lblMode.Text = "Add New Patient";
        }

        private void _LoadUpdateMode()
        {
            this.Text = "Update Patient";
            lblMode.Text = "Update Patient";
            lblPatientID.Text = _Patient.PatientID.ToString();
            ctrlRequiredTextBoxFirstName.Text = _Patient.FirstName;
            ctrlRequiredTextBoxLastName.Text = _Patient.LastName;
            mtxtPhone.Text = _Patient.Phone;
            txtEmail.Text = _Patient.Email;
            ctrlRequiredTextBoxNationalNo.Text = _Patient.NationalNo;
            _CurrentNationalNo = _Patient.NationalNo;
            mtxtEmergencyContact.Text = _Patient.EmergencyContact;
            ctrlRequiredTextBoxAddress.Text = _Patient.Address;
            cbGender.SelectedIndex = _Patient.Gender;
            dtpDateOfBirth.Value = _Patient.DateOfBirth;
        }

        private void _ValidateRequiredMtxt(object sender, CancelEventArgs e)
        {
            MaskedTextBox txt = (MaskedTextBox)sender;
            if (string.IsNullOrEmpty(txt.Text) || !txt.MaskCompleted)
            {
                e.Cancel = true;
                mtxtPhone.Focus();
                errorProvider1.SetError(txt, "This field is required");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txt, string.Empty);
            }
        }

        private void btnClose_Click(object sender, EventArgs e) => this.Close();

        private void frmAddEditPatient_Load(object sender, EventArgs e)
        {
            cbGender.SelectedIndex = 0;
            switch (this._Mode)
            {
                case enMode.AddNew:
                    _LoadAddNewMode();
                    break;
                case enMode.Update:
                    _LoadUpdateMode();
                    break;
            }
        }

        private bool _SaveInfo()
        {
            _Patient.FirstName = ctrlRequiredTextBoxFirstName.Text.Trim();
            _Patient.LastName = ctrlRequiredTextBoxLastName.Text.Trim();
            _Patient.DateOfBirth = dtpDateOfBirth.Value;
            _Patient.Phone = mtxtPhone.Text.Trim();
            _Patient.Email = txtEmail.Text.Trim();
            _Patient.NationalNo = ctrlRequiredTextBoxNationalNo.Text.Trim();
            _Patient.EmergencyContact = mtxtEmergencyContact.Text.Trim();
            _Patient.Address = ctrlRequiredTextBoxAddress.Text; 
            _Patient.Gender = (byte)cbGender.SelectedIndex;
            return _Patient.Save();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(!this.ValidateChildren())
            {
                MessageBox.Show("Check red circles.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (_SaveInfo())
            {
                MessageBox.Show("Patient Information is saved successfully.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblPatientID.Text = _Patient.PatientID.ToString();
                this._Mode = enMode.Update;
                this.Text = "Update Patient";
                lblMode.Text = "Update Patient";
                OnPatientAdd(_Patient);
            }
            else
                MessageBox.Show("An error occurred during information save.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void mtxtEmergencyContact_Validating(object sender, CancelEventArgs e)
        {
            _ValidateRequiredMtxt(sender, e);
            if (mtxtPhone.Text == mtxtEmergencyContact.Text)
            {
                e.Cancel = true;
                mtxtPhone.Focus();
                errorProvider1.SetError(mtxtEmergencyContact, "Emergency contact must be different from " +
                    "patient's number.");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(mtxtEmergencyContact, string.Empty);
            }
        }

        private void mtxtPhone_Validating_1(object sender, CancelEventArgs e) => _ValidateRequiredMtxt(sender, e);

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtEmail.Text.Trim()))
                return;

            if (!clsValidation.ValidateEmail(txtEmail.Text.Trim()))
            {
                e.Cancel = true;
                txtEmail.Focus();
                errorProvider1.SetError(txtEmail, "Enter a valid email.");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtEmail, string.Empty);
            }
        }

        private void ctrlRequiredTextBoxNationalNo_Validating(object sender, CancelEventArgs e)
        {
            if(clsPatient.DoesNationalNoExist(ctrlRequiredTextBoxNationalNo.Text.Trim()) &&
                ctrlRequiredTextBoxNationalNo.Text != _CurrentNationalNo)
            {
                e.Cancel = true;
                ctrlRequiredTextBoxNationalNo.Focus();
                errorProvider1.SetError(ctrlRequiredTextBoxNationalNo, "This National No. is used.");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(ctrlRequiredTextBoxNationalNo, string.Empty);
            }
        }
    }
}
