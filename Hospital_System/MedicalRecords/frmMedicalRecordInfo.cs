using System;
using System.Windows.Forms;

namespace Hospital_System.MedicalRecords
{
    public partial class frmMedicalRecordInfo : Form
    {
        private int _MedicalRecordID;
        public frmMedicalRecordInfo(int MedicalRecordID)
        {
            InitializeComponent();
            _MedicalRecordID = MedicalRecordID;
        }

        private void btnClose_Click(object sender, EventArgs e) => this.Close();

        private void frmMedicalRecordInfo_Load(object sender, EventArgs e) =>
            ctrlMedicalRecordInfo1.LoadMedicalRecordInfo(_MedicalRecordID);
    }
}
