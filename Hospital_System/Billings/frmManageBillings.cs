using Hospital_Business;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using Hospital_System.Properties;

namespace Hospital_System.Billings
{
    public partial class frmManageBillings : frmManageScreen
    {
        private DataTable _dtAllBillings;
        public frmManageBillings()
        {
            InitializeComponent();
            base.BTN.Visible = false;
        }

        private async Task _LoadDataAsync()
        {
            _dtAllBillings = await clsBilling.GetAllBillingsAysnc();
            base.DGV.DataSource = _dtAllBillings;
        }

        private void _LoadCMS()
        {
            base.CMS.Items.Add("Show Info");
            base.CMS.Items[0].Image = Resources.Manage_Appointments;
            base.CMS.Items.Add("Add Additional Charges");
            base.CMS.Items[1].Image = Resources.coin;
            base.CMS.Items.Add("Pay");
            base.CMS.Items[2].Image = Resources.coin;
            base.CMS.Opened += CMS_Opened;
            CMS.ItemClicked += CMS_ItemClicked;
        }

        private void CMS_Opened(object sender, EventArgs e)
        {
            int BillingID = (int)base.DGV.CurrentRow.Cells[0].Value;
            clsBilling CurrentBilling = clsBilling.Find(BillingID);

            if (CurrentBilling != null)
            {
                if(CurrentBilling.IsPaid)
                {
                    CMS.Items[1].Enabled = false;
                    CMS.Items[2].Enabled = false;
                }
                else
                {
                    CMS.Items[1].Enabled = true;
                    CMS.Items[2].Enabled = true;
                }
            }
        }

        private async void CMS_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "Show Info":
                    {
                        frmBillingInfo frm =
                        new frmBillingInfo((int)base.DGV.CurrentRow.Cells[0].Value);
                        frm.ShowDialog();
                    }
                    break;
                case "Add Additional Charges":
                    {
                        frmAddAditionalCharges frm = new frmAddAditionalCharges((int)base.DGV.CurrentRow.Cells[0].Value);
                        frm.ShowDialog();
                        await _RefreshAsync();
                    }
                    break;
                case "Pay":
                    {
                        frmPayBilling frm = new frmPayBilling((int)base.DGV.CurrentRow.Cells[0].Value);
                        frm.ShowDialog();
                        await _RefreshAsync();
                    }
                    break;
            }
        }

        private async Task _RefreshAsync()
        {
            base.Image = Resources.Manage_Billings;
            await _LoadDataAsync();

            if (base.DGV.Rows.Count > 0)
            {
                base.DGV.Columns[0].Width = 110;
                base.DGV.Columns[0].HeaderText = "Billing ID";

                base.DGV.Columns[1].Width = 120;
                base.DGV.Columns[1].HeaderText = "Appointment ID";

                base.DGV.Columns[2].Width = 120;
                base.DGV.Columns[2].HeaderText = "Consultation Fee";

                base.DGV.Columns[3].Width = 120;
                base.DGV.Columns[3].HeaderText = "Additional Charges";

                base.DGV.Columns[4].Width = 120;
                base.DGV.Columns[4].HeaderText = "Total Amount";

                base.DGV.Columns[5].Width = 110;
                base.DGV.Columns[5].HeaderText = "Is Paid";

                base.DGV.Columns[6].Width = 110;
                base.DGV.Columns[6].HeaderText = "Payment Method";
            }
            base.RecordsNumber = base.DGV.Rows.Count.ToString();
        }

        private async void frmManageBillings_Load(object sender, EventArgs e)
        {
            base.Image = Resources.Manage_Billings;
            await _LoadDataAsync();
            _LoadCMS();
            base.DGV.ContextMenuStrip = base.CMS;
            await _RefreshAsync();
        }
    }
}
