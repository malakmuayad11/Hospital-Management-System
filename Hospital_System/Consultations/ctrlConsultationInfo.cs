using Hospital_Business;
using System.Windows.Forms;

namespace Hospital_System.Consultations
{
    public partial class ctrlConsultationInfo : UserControl
    {
        private clsConsultation _Consultation;

        public ctrlConsultationInfo() => InitializeComponent();

        public void LoadConsultation(int ConsultationID)
        {
            _Consultation = clsConsultation.Find(ConsultationID);
            if (_Consultation == null)
            {
                MessageBox.Show("An error occurred during consultation loading.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            lblConsultationID.Text = _Consultation.ConsultationID.ToString();
            lblConsultationIName.Text = _Consultation.ConsultationName;
            lblConsultationFee.Text = _Consultation.ConsultationFee.ToString();
        }
    }
}
