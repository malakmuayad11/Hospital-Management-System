using Hospital_Business;
using System.Windows.Forms;

namespace Hospital_System.Appointments
{
    public partial class ctrlAppointmentInfo : UserControl
    {
        private clsAppointment _Appointment;
        public ctrlAppointmentInfo() => InitializeComponent();

        public void LoadAppointment(int AppointmentID)
        {
            _Appointment = clsAppointment.Find(AppointmentID);

            if(_Appointment == null)
            {
                MessageBox.Show("An error occurred while loading the appointment", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            lblAppointmentID.Text = AppointmentID.ToString();
            lblDoctorName.Text = _Appointment.DoctorInfo.FullName;
            lblPatientName.Text = _Appointment.PatientInfo.FullName;
            lblStatus.Text = _Appointment.StatusString;
            lblDate.Text = _Appointment.AppointmentDate.ToString(clsGlobal.DateFormat);
            lblTime.Text = _Appointment.AppointmentTime.ToString();
            lblReason.Text = _Appointment.ReasonForVisit;
        }
    }
}
