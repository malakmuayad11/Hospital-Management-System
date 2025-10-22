using Hospital_Business;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using Hospital_System.Properties;

namespace Hospital_System.MedicalRecords
{
    public partial class frmManageMedicalRecords : frmManageScreen 
    {
        private DataTable _dtAllMedicalRecords;
        public frmManageMedicalRecords()
        {
            InitializeComponent();
            base.BTN.Visible = false;
            this.Size = base.Size;
            this.Load += FrmManageMedicalRecords_Load;
        }

        private async void FrmManageMedicalRecords_Load(object sender, EventArgs e)
        {
            await _LoadDataAsync();
            _LoadCMS();
            base.DGV.ContextMenuStrip = base.CMS;
            await _RefreshAsync();
        }

        private async Task _LoadDataAsync()
        {
            _dtAllMedicalRecords = await clsMedicalRecord.GetAllMedicalRecordsAysnc();
            base.DGV.DataSource = _dtAllMedicalRecords;
        }

        private void _LoadCMS()
        {
            base.CMS.Items.Add("Show Info");
            base.CMS.Items[0].Image = Resources.Manage_Consultations;
            CMS.ItemClicked += CMS_ItemClicked;
        }

        private void CMS_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "Show Info":
                    {
                        frmMedicalRecordInfo frm = 
                            new frmMedicalRecordInfo(Convert.ToInt32(DGV.CurrentRow.Cells[0].Value));
                        frm.ShowDialog();
                    }
                    break;
            }
        }

        private async Task _RefreshAsync()
        {
            base.Image = Resources.Manage_Appointments;
            await _LoadDataAsync();

            if (base.DGV.Columns.Count > 0)
            {
                base.DGV.Columns[0].Width = 120;
                base.DGV.Columns[0].HeaderText = "Medical Record ID";

                base.DGV.Columns[1].Width = 110;
                base.DGV.Columns[1].HeaderText = "Appointment ID";

                base.DGV.Columns[2].Width = 120;
                base.DGV.Columns[2].HeaderText = "Symptoms";

                base.DGV.Columns[3].Width = 120;
                base.DGV.Columns[3].HeaderText = "Diagnosis";

                base.DGV.Columns[4].Width = 140;
                base.DGV.Columns[4].HeaderText = "Medical Record Notes";
            }
            base.RecordsNumber = base.DGV.Rows.Count.ToString();
        }
    }
}
