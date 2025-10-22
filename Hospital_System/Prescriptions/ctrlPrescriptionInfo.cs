using Hospital_Business;
using System.Windows.Forms;

namespace Hospital_System.Prescription
{
    public partial class ctrlPrescriptionInfo : UserControl
    {
        private clsPrescription _Prescription;
        public ctrlPrescriptionInfo() => InitializeComponent();

        public void LoadPrescriptionInfo(int AppointmentID)
        {
            _Prescription = clsPrescription.FindByAppointmentID(AppointmentID);

            if(_Prescription == null)
            {
                MessageBox.Show("An error occurred while loading prescription info", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            lblAppointmentID.Text = AppointmentID.ToString();
            lblPrescriptionID.Text = _Prescription.PrescriptionID.ToString();
            lblMedicationName.Text = _Prescription.MedicationName;
            lblDosage.Text = _Prescription.Dosage;
            lblDurationDays.Text = _Prescription.DurationDays.ToString();
            lblDurationMonths.Text = _Prescription.DurationMonths.ToString();
        }
    }
}
