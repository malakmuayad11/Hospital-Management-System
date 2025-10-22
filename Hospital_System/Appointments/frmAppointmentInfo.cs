using System;
using System.Windows.Forms;

namespace Hospital_System.Appointments
{
    public partial class frmAppointmentInfo : Form
    {
        private int _AppointmentID;
        public frmAppointmentInfo(int AppointmentID)
        {
            InitializeComponent();
            _AppointmentID = AppointmentID;
        }

        private void btnClose_Click(object sender, EventArgs e) => this.Close();

        private void frmAppointmentInfo_Load(object sender, EventArgs e) => ctrlAppointmentInfo1.LoadAppointment(_AppointmentID);
    }
}
