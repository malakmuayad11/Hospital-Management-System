using System;
using Hospital_Business;
using System.Windows.Forms;

namespace Hospital_System
{
    public partial class frmAddEditUser2 : Form
    {
        private clsUser _User;
        private int _UserID;

        private enum enMode { AddNew = 1, Update = 2 };
        private enMode _Mode;

        public frmAddEditUser2()
        {
            InitializeComponent();
            _User = new clsUser();
            this._Mode = enMode.AddNew;
            //checkedListBox1.CheckOnClick = true;
        }

        public frmAddEditUser2(int UserID)
        {
            InitializeComponent();
            this._UserID = UserID;
            _User = clsUser.Find(_UserID);
            this._Mode = enMode.Update;
            //checkedListBox1.CheckOnClick = true;
        }

        private void _LoadAddNewMode()
        {
            this.Text = "Add New User";
            lblMode.Text = "Add New User";
            cbRole.SelectedIndex = 0;
        }

        private void _LoadUpdateMode()
        {
            this.Text = "Update User";
            lblMode.Text = "Update User";
            lblUserID.Text = _User.UserID.ToString();
            ctrlRequiredTextBoxUsername.Text = _User.Username;
            cbRole.SelectedIndex = _User.Role - 1;
            //handle permissions
            ctrlRequiredTextBoxNewPassword.Enabled = false;
            ctrlRequiredTextBoxConfirmPassword.Enabled = false;
        }

        private void btnClose_Click(object sender, EventArgs e) => this.Close();

        private void frmAddEditUser_Load(object sender, EventArgs e)
        {
            ctrlRequiredTextBoxNewPassword.PasswordChar = '*';
            ctrlRequiredTextBoxConfirmPassword.PasswordChar = '*';

            switch (this._Mode)
            {
                case enMode.AddNew:
                    _LoadAddNewMode();
                    break;
                case enMode.Update:
                    _LoadUpdateMode();
                    break;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _User.Username = ctrlRequiredTextBoxUsername.Text.Trim();
            if (_Mode == enMode.AddNew)
                _User.Password = ctrlRequiredTextBoxConfirmPassword.Text.Trim();
            _User.Role = Convert.ToByte(cbRole.SelectedIndex + 1);
            _User.Permissions = clsUser.enPermissions.eAll; //should be modified

            if (_User.Save())
            {
                MessageBox.Show("User Information is saved successfully.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                this._Mode = enMode.Update;
                this.Text = "Update";
                lblMode.Text = "Update User";
            }
            else
                MessageBox.Show("An error occurred during information save.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ctrlRequiredTextBoxConfirmPassword_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (ctrlRequiredTextBoxNewPassword.Text.Trim() !=
                ctrlRequiredTextBoxConfirmPassword.Text.Trim())
            {
                e.Cancel = true;
                ctrlRequiredTextBoxConfirmPassword.Focus();
                errorProvider1.SetError(ctrlRequiredTextBoxConfirmPassword,
                    "Confirm Password does not match.");
            }
        }

        private void ctrlRequiredTextBoxUsername_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_Mode == enMode.Update)
                return;

            if (clsUser.IsUsernameUsed(ctrlRequiredTextBoxUsername.Text.Trim()))
            {
                e.Cancel = true;
                ctrlRequiredTextBoxUsername.Focus();
                errorProvider1.SetError(ctrlRequiredTextBoxUsername, "This username is already used, choose another one!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(ctrlRequiredTextBoxUsername, string.Empty);
            }
        }
    }
}
