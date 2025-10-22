using Hospital_Business;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using Hospital_System.Properties;

namespace Hospital_System.Consultations
{
    public partial class frmManageConsultations : frmManageScreen
    {
        private DataTable _dtAllConsultations;
        public frmManageConsultations()
        {
            InitializeComponent();
            base.BTN.Visible = false;
        }
        private async Task _LoadDataAsync()
        {
            _dtAllConsultations = await clsConsultation.GetAllConsultationsAsync();
            base.DGV.DataSource = _dtAllConsultations;
        }

        private async Task _RefreshAsync()
        {
            base.Image = Resources.Manage_Consultations;
            await _LoadDataAsync();

            if (base.DGV.Rows.Count > 0)
            {
                base.DGV.Columns[0].Width = 150;
                base.DGV.Columns[0].HeaderText = "Consultation ID";

                base.DGV.Columns[1].Width = 240;
                base.DGV.Columns[1].HeaderText = "Consultation Name";

                base.DGV.Columns[2].Width = 200;
                base.DGV.Columns[2].HeaderText = "Consultation Fee";
            }
            base.RecordsNumber = base.DGV.Rows.Count.ToString();
        }
        private void _LoadCMS()
        {
             base.CMS.Items.Add("Show Info");
             base.CMS.Items[0].Image = Resources.Manage_Consultations;
             CMS.ItemClicked += CMS_ItemClicked;
        }

        private void CMS_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            frmConsultationInfo frm =
            new frmConsultationInfo((int)base.DGV.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            
        }

        private async void frmManageConsultations_Load(object sender, EventArgs e)
        {
            await _LoadDataAsync();
            _LoadCMS();
            base.DGV.ContextMenuStrip = base.CMS;
            await _RefreshAsync();
        }
    }
}
