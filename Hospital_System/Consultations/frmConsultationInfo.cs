using System;
using System.Windows.Forms;

namespace Hospital_System.Consultations
{
    public partial class frmConsultationInfo : Form
    {
        private int _ConsultationID;

        public frmConsultationInfo(int ConsultationID)
        {
            InitializeComponent();
            this._ConsultationID = ConsultationID;
        }

        private void btnClose_Click(object sender, EventArgs e) => this.Close();

        private void frmConsultationInfo_Load(object sender, EventArgs e) =>
            ctrlConsultationInfo1.LoadConsultation(_ConsultationID);
    }
}
