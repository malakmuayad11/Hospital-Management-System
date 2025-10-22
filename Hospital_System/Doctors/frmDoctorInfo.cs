using System;
using System.Windows.Forms;

namespace Hospital_System.Doctors
{
    public partial class frmDoctorInfo : Form
    {
        private int _DoctorID;
        public frmDoctorInfo(int doctorID)
        {
            InitializeComponent();
            _DoctorID = doctorID;
        }

        private void frmDoctorInfo_Load(object sender, EventArgs e) => ctrlDoctorInfo1.LoadDoctorInfo(_DoctorID);

        private void btnClose_Click(object sender, EventArgs e) => this.Close();
    }
}
