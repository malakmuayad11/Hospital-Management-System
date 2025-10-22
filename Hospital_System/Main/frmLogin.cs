using Hospital_Business;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;

namespace Hospital_System
{
    public partial class frmLogin : Form
    {
        public frmLogin() => InitializeComponent();

        private void btnClose_Click(object sender, EventArgs e) => this.Close();

        private void frmLogin_Load(object sender, EventArgs e)
        {
            ctrlRequiredTextBoxPassword.PasswordChar = '*';
            ctrlRequiredTextBoxUsername.Text = "Username";
            ctrlRequiredTextBoxPassword.Text = "Password";
            _FillLoginFields();
            ctrlRequiredTextBoxPassword.TextChanged += CtrlRequiredTextBoxPassword_TextChanged;
        }

        private void CtrlRequiredTextBoxPassword_TextChanged(object sender, EventArgs e) =>
            ctrlRequiredTextBoxPassword.PasswordChar = '*';

        private void ctrlRequiredTextBoxUsername_Validating(object sender, CancelEventArgs e)
        {
            if(ctrlRequiredTextBoxUsername.Text.Trim() == "Username")
            {
                e.Cancel = true;
                ctrlRequiredTextBoxUsername.Focus();
                errorProvider1.SetError(ctrlRequiredTextBoxUsername, "Please enter a valid username");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(ctrlRequiredTextBoxUsername, string.Empty);
            }
        }

        private void ctrlRequiredTextBoxPassword_Validating(object sender, CancelEventArgs e)
        {
            if (ctrlRequiredTextBoxPassword.Text.Trim() == "Password")
            {
                e.Cancel = true;
                ctrlRequiredTextBoxUsername.Focus();
                errorProvider1.SetError(ctrlRequiredTextBoxPassword, "Please enter a valid password");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(ctrlRequiredTextBoxPassword, string.Empty);
            }
        }

        private static (string Username, string Password) _GetUsernameAndPassword()
        {
            string Username = string.Empty;
            string Password = string.Empty; 
            try
            {
                Username = clsGlobal.ReadFromRegistry("Username");
                Password = clsGlobal.ReadFromRegistry("Password");
            }
            catch (Exception ex)
            {
                clsLogger.Log(ex.Message, EventLogEntryType.Error);
            }
            return (Username, Password);
        }

        private void _FillLoginFields()
        {
            (string Username, string Password) = _GetUsernameAndPassword();

            ctrlRequiredTextBoxUsername.Text = string.IsNullOrEmpty(Username) ? "Username" : Username;

            if (!string.IsNullOrEmpty(Password))
                ctrlRequiredTextBoxPassword.Text = clsGlobal.Decrypt_AES(Password, clsGlobal.Key);
            else
            {
                ctrlRequiredTextBoxPassword.PasswordChar = '\0';
                ctrlRequiredTextBoxPassword.Text = "Password";
            }
        }

        private void _AddCurrentUserForAuditTrailing()
        {
            if (!clsGlobal.CurrentUser.AddAsCurrentUser())// Add the current user in the database for audit trailing.
                MessageBox.Show("An error occured while logging in, try again", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void _EnterMainScreen()
        {
            if (clsGlobal.CurrentUser.Role == (byte)clsUser.enRole.Doctor)
            {
                frmDoctorMainScreen frm2 = new frmDoctorMainScreen(this, clsGlobal.CurrentUser.UserID);
                frm2.ShowDialog();
            }
            else
            {
                frmMainScreen frm = new frmMainScreen(this);
                frm.ShowDialog();
            }
        }

        private void _Login()
        {
            clsGlobal.CurrentUser = clsUser.Find(ctrlRequiredTextBoxUsername.Text.Trim(),
                clsUtil.ComputeHash(ctrlRequiredTextBoxPassword.Text.Trim()));
            if (clsGlobal.CurrentUser == null)
            {
                MessageBox.Show("Invalide Username/Password", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _AddCurrentUserForAuditTrailing();
            clsGlobal.CurrentUser.UpdateUserLastLoginDate();
            _RememberMe();

            this.Visible = false;
            //Check role, and open the appropriate screen.
            _EnterMainScreen();
        }

        private void btnLogin_Click(object sender, EventArgs e) => _Login();

        private void _RememberMe()
        {
            if (chkRememberMe.Checked)
            {
                if (string.IsNullOrEmpty(ctrlRequiredTextBoxUsername.Text)
                    || string.IsNullOrEmpty(ctrlRequiredTextBoxPassword.Text))
                    return;

                clsGlobal.WriteInRegistry("Username", ctrlRequiredTextBoxUsername.Text.Trim());
                clsGlobal.WriteInRegistry("Password", clsGlobal.Encrypt_AES(ctrlRequiredTextBoxPassword.Text.Trim(),
                    clsGlobal.Key));
            }
            else
            {
                clsGlobal.WriteInRegistry("Username", string.Empty);
                clsGlobal.WriteInRegistry("Password", string.Empty);
            }
        }
    }
}
