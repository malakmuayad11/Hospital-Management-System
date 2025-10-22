using Hospital_Business;
using System.Windows.Forms;

namespace Hospital_System.Billings
{
    public partial class ctrlBillingInfo : UserControl
    {
        private clsBilling _Billing;
        public ctrlBillingInfo() => InitializeComponent();

        public string IsPaid
        {
            get => lblIsPaid.Text;
            set => lblIsPaid.Text = value;
        }

        public void LoadBillingInfo(int BillingID)
        {
            _Billing = clsBilling.Find(BillingID);

            if (_Billing == null)
            {
                MessageBox.Show("An error occurred during billing loading.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            lblBillingID.Text = BillingID.ToString();
            lblAppointmentID.Text = _Billing.AppointmentID.ToString();
            lblConsultationFee.Text = _Billing.ConsulationFee.ToString();
            lblAdditionalCharges.Text = (_Billing.AdditionalCharges == null) ?
                "No additional charges" : _Billing.AdditionalCharges.ToString();
            lblTotalAmount.Text = _Billing.TotalAmount.ToString();
            lblIsPaid.Text = _Billing.IsPaid ? "Paid" : "Not Paid";
            lblPaymentMethod.Text = _Billing.PaymentMethodString?? "Not Paid";
        }
    }
}
