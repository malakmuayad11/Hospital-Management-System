using System;
using System.Windows.Forms;

namespace Hospital_System.Patients
{
    public partial class frmPatientInfo : Form
    {
        private int _PatientID;
        public frmPatientInfo(int PatientID)
        {
            InitializeComponent();
            _PatientID = PatientID;
        }

        private void btnClose_Click(object sender, EventArgs e) => this.Close();

        private void frmPatientInfo_Load(object sender, EventArgs e) => ctrlPatientInfo1.LoadPatientInfo(_PatientID);
    }
}
