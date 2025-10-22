using Hospital_Business;
using System;
using System.Windows.Forms;

namespace Hospital_System.Billings
{
    public partial class frmAddAditionalCharges : Form
    {
        private int _BillingID;
        private clsBilling _Billing;
        public frmAddAditionalCharges(int BillingID)
        {
            InitializeComponent();
            _BillingID = BillingID;
            _Billing = clsBilling.Find(BillingID);
        }

        private void btnClose_Click(object sender, EventArgs e) => this.Close();

        private void frmAddAditionalCharges_Load(object sender, EventArgs e)
        {
            if (_Billing != null)
            {
                lblBillingID.Text = _BillingID.ToString();
                lblAppointmentID.Text = _Billing.AppointmentID.ToString();
            }
            else
            {
                MessageBox.Show("An error occurred during loading the billing.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(_Billing.UpdateBillingCharges(_BillingID, Convert.ToDecimal(nudAdditionalCharges.Value)))
                MessageBox.Show("Billing's charges are updated successfully", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Billing's charges are not updated, an error occurred", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
