using System;
using System.Windows.Forms;

namespace Hospital_System.Billings
{
    public partial class frmBillingInfo : Form
    {
        private int _BillingID;
        public frmBillingInfo(int BillingID)
        {
            InitializeComponent();
            _BillingID = BillingID;
        }

        private void btnClose_Click(object sender, EventArgs e) => this.Close();

        private void frmBillingInfo_Load(object sender, EventArgs e) =>
            ctrlBillingInfo1.LoadBillingInfo(_BillingID);
    }
}
