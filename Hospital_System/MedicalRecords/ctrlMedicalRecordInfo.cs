using Hospital_Business;
using Hospital_System.Prescriptions;
using System.Windows.Forms;

namespace Hospital_System.MedicalRecords
{
    public partial class ctrlMedicalRecordInfo : UserControl
    {
        private clsMedicalRecord _MedicalRecord;
        private int _AppointmentID;
        public ctrlMedicalRecordInfo() => InitializeComponent();

        public void LoadMedicalRecordInfo(int MedicalRecordID)
        {
            _MedicalRecord = clsMedicalRecord.Find(MedicalRecordID);

            if(_MedicalRecord == null)
            {
                MessageBox.Show("An error occured while record loading", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                llPrescriptionInfo.Enabled = false;
                return;
            }
            lblMedicalRecordID.Text = MedicalRecordID.ToString();
            lblAppointmentID.Text = _MedicalRecord.AppointmentID.ToString();
            _AppointmentID = _MedicalRecord.AppointmentID;
            lblSymptoms.Text = _MedicalRecord.Symptoms;
            lblDiagnosis.Text = _MedicalRecord.Diagnosis;
            txtNotes.Text = _MedicalRecord.MedicalRecordNotes ?? string.Empty;
        }

        private void llPrescriptionInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmPrescriptionInfo frm = new frmPrescriptionInfo(_AppointmentID);
            frm.ShowDialog();
        }
    }
}
