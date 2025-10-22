using System;
using System.Windows.Forms;

namespace Hospital_System.Prescriptions
{
    public partial class frmPrescriptionInfo : Form
    {
        private int _AppointmentID;
        public frmPrescriptionInfo(int AppointmentID)
        {
            InitializeComponent();
            _AppointmentID = AppointmentID;
        }

        private void btnClose_Click(object sender, EventArgs e) => this.Close();

        private void frmPrescriptionInfo_Load(object sender, EventArgs e) =>
            ctrlPrescriptionInfo1.LoadPrescriptionInfo(_AppointmentID);
    }
}
