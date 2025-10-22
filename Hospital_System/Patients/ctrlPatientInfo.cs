using Hospital_Business;
using System;
using System.Windows.Forms;

namespace Hospital_System.Patients
{
    public partial class ctrlPatientInfo : UserControl
    {
        public int PatientID {  get => Convert.ToInt32(lblPatientID.Text.Trim()); } 
        public class PatientEventArgs : EventArgs
        {
            public bool IsFound { get; set; }
            public PatientEventArgs(bool IsFound)
            {
                this.IsFound = IsFound;
            }
        }

        private clsPatient _Patient;

        public event EventHandler<PatientEventArgs> OnPatientLoad;
        public ctrlPatientInfo() => InitializeComponent();

        protected virtual void PatientLoad(PatientEventArgs e) => OnPatientLoad?.Invoke(this, e);

        public void PatientLoad(bool IsFound) => PatientLoad(new PatientEventArgs(IsFound));

        private void _LoadFields()
        {
            lblPatientID.Text = _Patient.PatientID.ToString();
            lblName.Text = _Patient.FullName;
            lblNationalNo.Text = _Patient.NationalNo;
            lblGender.Text = _Patient.Gender == 0 ? "Male" : "Female";
            lblDateOfBirth.Text = _Patient.DateOfBirth.ToString(clsGlobal.DateFormat);
            txtAddress.Text = _Patient.Address;
            lblEmergencyContact.Text = _Patient.EmergencyContact;
        }

        public void LoadPatientInfo(int PatientID)
        {
            _Patient = clsPatient.Find(PatientID);
            if(_Patient == null)
            {
                MessageBox.Show("An error during during patient info loading", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                PatientLoad(false);
                return;
            }
            _LoadFields();
            PatientLoad(true);
        }

        public void LoadPatientInfo(string NationalNo)
        {
            _Patient = clsPatient.Find(NationalNo);
            if (_Patient == null)
            {
                MessageBox.Show("An error during during patient info loading", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                PatientLoad(false);
                return;
            }
            _LoadFields();
            PatientLoad(true);
        }
    }
}
