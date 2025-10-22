using Hospital_Business;
using Hospital_System.Doctors;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using Hospital_System.Properties;

namespace Hospital_System
{
    public partial class frmManageDoctors : frmManageScreen
    {
        private DataTable _dtAllDoctors;
        public frmManageDoctors()
        {
            InitializeComponent();
            this.Size = base.Size;
        }

        private async Task _LoadDataAsync()
        {
            _dtAllDoctors = await clsDoctor.GetAllDoctorsAsync();
            base.DGV.DataSource = _dtAllDoctors;
        }

        private void _LoadCMS()
        {
            base.CMS.Items.Add("Show Info");
            base.CMS.Items[0].Image = Resources.Manage_Doctors;
            base.CMS.Items.Add("Edit");
            base.CMS.Items[1].Image = Resources.rescheduling;
            base.CMS.Items.Add("Delete Doctor");
            base.CMS.Items[2].Image = Resources.cross_32;
            CMS.ItemClicked += CMS_ItemClicked;
            CMS.Opened += CMS_Opened;
            BTN.Click += BTN_Click;
        }

        private void CMS_Opened(object sender, EventArgs e)
        {
            if(!clsValidation.DoesCurrentUserHavePermission(clsUser.enPermissions.eAddEditDoctors))
            {
                CMS.Items[1].Enabled = false; // Edit doctor
                CMS.Items[2].Enabled = false; // Delete Doctor
            }
        }

        private async void BTN_Click(object sender, EventArgs e)
        {
            if (clsValidation.DoesCurrentUserHavePermission(clsUser.enPermissions.eAddEditDoctors))
            {
                frmAddEditDoctor frm = new frmAddEditDoctor();
                frm.ShowDialog();
                await _RefreshAsync();
            }
        }

        private async Task _DeleteDoctorAsync()
        {
            if (clsDoctor.Delete((int)base.DGV.CurrentRow.Cells[0].Value))
            {
                MessageBox.Show("Doctor is deleted successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                await _RefreshAsync();
            }
            else
                MessageBox.Show("An error occureed druing doctor delete.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private async void CMS_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "Show Info":
                    {
                        frmDoctorInfo frm =
                        new frmDoctorInfo((int)base.DGV.CurrentRow.Cells[0].Value);
                        frm.ShowDialog();
                    }
                    break;
                case "Edit":
                    {
                        frmAddEditDoctor frm = new frmAddEditDoctor((int)base.DGV.CurrentRow.Cells[0].Value);
                        frm.ShowDialog();
                        await _RefreshAsync();
                    }
                    break;
                case "Delete Doctor":
                    {
                        if (MessageBox.Show("Are you sure you want to delete this doctor?", "Confirm",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            await _DeleteDoctorAsync();
                    }
                    break;
            }
        }

        private async Task _RefreshAsync()
        {
            await _LoadDataAsync();

            if (base.DGV.Rows.Count > 0)
            {
                base.DGV.Columns[0].Width = 110;
                base.DGV.Columns[0].HeaderText = "Doctor ID";

                base.DGV.Columns[1].Width = 120;
                base.DGV.Columns[1].HeaderText = "Name";

                base.DGV.Columns[2].Width = 120;
                base.DGV.Columns[2].HeaderText = "Start Work Day";

                base.DGV.Columns[3].Width = 120;
                base.DGV.Columns[3].HeaderText = "End Work Day";

                base.DGV.Columns[4].Width = 120;
                base.DGV.Columns[4].HeaderText = "Start Work Hour";

                base.DGV.Columns[5].Width = 110;
                base.DGV.Columns[5].HeaderText = "End Work Hour";

                base.DGV.Columns[6].Width = 110;
                base.DGV.Columns[6].HeaderText = "Specialty";
            }
            base.RecordsNumber = base.DGV.Rows.Count.ToString();
        }

        private async void frmManageDoctors_Load(object sender, EventArgs e)
        {
            base.Image = Resources.Manage_Doctors;
            await _LoadDataAsync();
            _LoadCMS();
            base.DGV.ContextMenuStrip = base.CMS;
            await _RefreshAsync();
        }
    }
}
