using Hospital_Business;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Hospital_System.Billings
{
    public partial class frmPayBilling : Form
    {
        private int _BillingID;
        private clsBilling _Billing;
        public frmPayBilling(int BillingID)
        {
            InitializeComponent();
            _BillingID = BillingID;
            _Billing = clsBilling.Find(BillingID);
        }

        private void btnClose_Click(object sender, EventArgs e) => this.Close();

        private void cbPaymentMethod_Validating(object sender, CancelEventArgs e)
        {
            if(cbPaymentMethod.SelectedIndex == -1)
            {
                e.Cancel = true;
                cbPaymentMethod.Focus();
                errorProvider1.SetError(cbPaymentMethod, "Payment method should be selected.");
            }
            else
            {
                e.Cancel= false;
                errorProvider1.SetError(cbPaymentMethod, string.Empty);
            }
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren()) return;

            if(_Billing.UpdateBillingPaymentStatus(_BillingID, true, Convert.ToByte(cbPaymentMethod.SelectedIndex)))
            {
                 MessageBox.Show("Billing is paid successfully", "Success",
                     MessageBoxButtons.OK, MessageBoxIcon.Information);
                 btnPay.Enabled = false;
                 cbPaymentMethod.Enabled = false;
                ctrlBillingInfo1.IsPaid = "Paid";
            }
            else
                 MessageBox.Show("An error occurred, and the billing isn't paid", "Error",
                     MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void frmPayBilling_Load(object sender, EventArgs e)
        {
            ctrlBillingInfo1.LoadBillingInfo(_BillingID);
            if(_Billing == null)
            {
                MessageBox.Show("An error occured while loading the billing", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }
    }
}
