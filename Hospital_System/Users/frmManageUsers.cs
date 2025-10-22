using Hospital_Business;
using Hospital_System.Properties;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hospital_System
{
    public partial class frmManageUsers : frmManageScreen
    {
        private DataTable _dtUsers;

        public frmManageUsers() => InitializeComponent();

        private async Task _LoadDataAsync()
        {
            _dtUsers = await clsUser.GetAllUsersAysnc();
            base.DGV.DataSource = _dtUsers;
        }

        private void _LoadCMS()
        {
            base.CMS.Items.Add("Show Info");
            base.CMS.Items[0].Image = Resources.User_32__2;
            base.CMS.Items.Add("Edit");
            base.CMS.Items[1].Image = Resources.rescheduling;
            base.CMS.Items.Add("Change Password");
            base.CMS.Items[2].Image = Resources.rescheduling;
            base.CMS.Items.Add("Delete User");
            base.CMS.Items[3].Image = Resources.cross_32;
            CMS.ItemClicked += CMS_ItemClicked;
            BTN.Click += BTN_Click;
        }

        private async void BTN_Click(object sender, EventArgs e)
        {
            frmAddEditUser frm = new frmAddEditUser();
            frm.ShowDialog();
            await _RefreshAsync();
        }

        private async Task _DeleteUserAsync()
        {
            if (clsUser.DeleteUser((int)base.DGV.CurrentRow.Cells[0].Value))
            {
                MessageBox.Show("User is deleted successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                await _RefreshAsync();
            }
            else
                MessageBox.Show("An error occureed druing user delete.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private async void CMS_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "Show Info":
                    {
                        frmUserInfo frm =
                        new frmUserInfo((int)base.DGV.CurrentRow.Cells[0].Value);
                        frm.ShowDialog();
                    }
                    break;
                case "Edit":
                    {
                        frmAddEditUser frm = new frmAddEditUser((int)base.DGV.CurrentRow.Cells[0].Value);
                        frm.ShowDialog();
                        await _RefreshAsync();
                    }
                    break;
                case "Change Password":
                    {
                        frmChangePassword frm =
                        new frmChangePassword((int)base.DGV.CurrentRow.Cells[0].Value);
                        frm.ShowDialog();
                    }
                    break;
                case "Delete User":
                    {
                        if(MessageBox.Show("Are you sure you want to delete this user account?", "Confirm", 
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            await _DeleteUserAsync();
                    }
                    break;
            }
        }

        private async Task _RefreshAsync()
        {
            await _LoadDataAsync();

            if (base.DGV.Columns.Count > 0)
            {
                base.DGV.Columns[0].Width = 110;
                base.DGV.Columns[0].HeaderText = "User ID";

                base.DGV.Columns[1].Width = 120;
                base.DGV.Columns[1].HeaderText = "Username";

                base.DGV.Columns[2].Width = 120;
                base.DGV.Columns[2].HeaderText = "Role";

                base.DGV.Columns[3].Width = 120;
                base.DGV.Columns[3].HeaderText = "Last Login Date";
            }
            base.RecordsNumber = base.DGV.Rows.Count.ToString();
        }

        private async void frmManageUsers_Load(object sender, EventArgs e)
        {
            base.Image = Resources.Manage_People;
            await _LoadDataAsync();
            _LoadCMS();
            base.DGV.ContextMenuStrip = base.CMS;
            await _RefreshAsync();
        }
    }
}
