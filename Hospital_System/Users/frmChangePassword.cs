using Hospital_Business;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Hospital_System
{
    public partial class frmChangePassword : Form
    {
        private int _UserID;
        public frmChangePassword(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;
        }

        private void btnClose_Click(object sender, EventArgs e) => this.Close();

        private void frmChangePassword_Load(object sender, EventArgs e) =>
            ctrlUserInfo1.LoadUserData(_UserID);

        private void txtCurrentPassword_Validating(object sender, CancelEventArgs e)
        {
            clsValidation.ValidateRequiredTextBox(sender, errorProvider1, e);
            if (clsUtil.ComputeHash(txtCurrentPassword.Text.Trim()) != ctrlUserInfo1.User.Password)
            {
                txtCurrentPassword.Focus();
                e.Cancel = true;
                errorProvider1.SetError(txtCurrentPassword, "The current password isn't correct");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtCurrentPassword, string.Empty);
            }
        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            clsValidation.ValidateRequiredTextBox(sender, errorProvider1, e);

            if (txtNewPassword.Text.Trim() != txtConfirmPassword.Text.Trim())
            {
                txtNewPassword.Focus();
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "Confirm password doesn't match the new one.");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError (txtConfirmPassword, string.Empty);
            }
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Please check red circle(s).", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (ctrlUserInfo1.User.ChangePassword(clsUtil.ComputeHash(txtConfirmPassword.Text.Trim())))
            {
                txtCurrentPassword.Enabled = false;
                txtNewPassword.Enabled = false;
                txtConfirmPassword.Enabled = false;

                MessageBox.Show("Password is changed successfully", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("You used the new password before, enter another one.", "Not Allowed", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void txtNewPassword_Validating(object sender, CancelEventArgs e) => clsValidation.ValidateRequiredTextBox(sender, errorProvider1, e);
    }
}
