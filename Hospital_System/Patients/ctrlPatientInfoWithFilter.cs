using System;
using System.Windows.Forms;

namespace Hospital_System.Patients
{
    public partial class ctrlPatientInfoWithFilter : UserControl
    {
        private bool _IsPatientFound = false;

        public int PatientID { get => ctrlPatientInfo1.PatientID; }

        public string Phone { get => ctrlPatientInfo1.Phone; }

        public event EventHandler<bool> FindPatient;

        public void OnFindPatient(bool IsPatientFound) =>
            FindPatient?.Invoke(this, IsPatientFound);
        public ctrlPatientInfoWithFilter() => InitializeComponent();

        private void CtrlPatientInfo1_OnPatientLoad(object sender, ctrlPatientInfo.PatientEventArgs e) =>
            _IsPatientFound = e.IsFound;

        private void btnAddNewPatient_Click(object sender, EventArgs e)
        {
            if (!clsValidation.DoesCurrentUserHavePermission(Hospital_Business.clsUser.enPermissions.eManagePatients))
                return;

            frmAddEditPatient frm = new frmAddEditPatient();
            frm.PatientAdded += Frm_PatientAdded; 
            frm.ShowDialog();
        }

        private void Frm_PatientAdded(object sender, Hospital_Business.clsPatient Patient) 
        {
            ctrlPatientInfo1.LoadPatientInfo(Patient.PatientID);
            txtNationalNo.Text = Patient.NationalNo;
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNationalNo.Text.Trim()))
            {
                ctrlPatientInfo1.OnPatientLoad += CtrlPatientInfo1_OnPatientLoad;
                ctrlPatientInfo1.LoadPatientInfo(txtNationalNo.Text.Trim());
                OnFindPatient(_IsPatientFound);
            }
            else
                MessageBox.Show("Please enter the National No", "National No is empty",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
}
