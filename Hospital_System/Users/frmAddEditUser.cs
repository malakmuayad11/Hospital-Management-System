using System;
using Hospital_Business;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Hospital_System
{
    public partial class frmAddEditUser : Form
    {
        private clsUser _User;
        private int _UserID;
        private List<CheckBox> _Checkboxes;

        private enum enMode { AddNew = 1, Update = 2 };
        private enMode _Mode;

        public frmAddEditUser()
        {
            InitializeComponent();
            _User = new clsUser();
            this._Mode = enMode.AddNew;

            _Checkboxes = new List<CheckBox>()
            {
                chkManagePatients,
                chkManageAppointments,
                chkManagePayments,
                chkAddEditMedicalRecords,
                chkShowMedicalRecords,
                chkAddEditDoctors,
                chkManageUsers
            };
        }

        public frmAddEditUser(int UserID)
        {
            InitializeComponent();
            this._UserID = UserID;
            _User = clsUser.Find(_UserID);
            this._Mode = enMode.Update;

            _Checkboxes = new List<CheckBox>()
            {
                chkManagePatients,
                chkManageAppointments,
                chkManagePayments,
                chkAddEditMedicalRecords,
                chkShowMedicalRecords,
                chkAddEditDoctors,
                chkManageUsers
            };
        }

        private void _LoadAddNewMode()
        {
            this.Text = "Add New User";
            lblMode.Text = "Add New User";
            cbRole.SelectedIndex = 1;
        }

        private void _LoadUpdateMode()
        {
            this.Text = "Update User";
            lblMode.Text = "Update User";
            lblUserID.Text = _User.UserID.ToString();
            ctrlRequiredTextBoxUsername.Text = _User.Username;
            cbRole.SelectedIndex = _User.Role - 1;
            _LoadPermissions();
            // For validation in save button, we will make the text the same:
            ctrlRequiredTextBoxConfirmPassword.Text = "Enter Password";
            ctrlRequiredTextBoxNewPassword.Text = "Enter Password";

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

        private void _LoadPermissions()
        {
            if (_User.Role == (byte)clsUser.enRole.Doctor)
            {
                chkAddEditMedicalRecords.Checked = true;
                chkShowMedicalRecords.Checked = true;
                chkManageAppointments.Checked = true;
            }
            else if (_User.Role == (byte)clsUser.enRole.Admin)
                chkAll.Checked = true;
            else
            {
                chkAddEditDoctors.Checked = clsUser.DoesUserHavePersmissions(_User, clsUser.enPermissions.eAddEditDoctors);
                chkManagePatients.Checked = clsUser.DoesUserHavePersmissions(_User, clsUser.enPermissions.eManagePatients);
                chkManageAppointments.Checked = clsUser.DoesUserHavePersmissions(_User, clsUser.enPermissions.eManageAppointments);
                chkManagePayments.Checked = clsUser.DoesUserHavePersmissions(_User, clsUser.enPermissions.eManagePayments);
                chkShowMedicalRecords.Checked = clsUser.DoesUserHavePersmissions(_User, clsUser.enPermissions.eShowMedicalRecords);
                chkAddEditMedicalRecords.Checked = clsUser.DoesUserHavePersmissions(_User, clsUser.enPermissions.eAddEditMedicalRecords);
                chkManageUsers.Checked = clsUser.DoesUserHavePersmissions(_User, clsUser.enPermissions.eManageUsers);
            }
        }

        private int _GetSelectedPermiessions()
        {
            if (chkAll.Checked)
                return -1;

            int Permissions = 0;

            if (chkAddEditDoctors.Checked)
                Permissions += (int)clsUser.enPermissions.eAddEditDoctors;
            if (chkManagePatients.Checked)
                Permissions += (int)clsUser.enPermissions.eManagePatients;
            if (chkManageAppointments.Checked)
                Permissions += (int)clsUser.enPermissions.eManageAppointments;
            if (chkManagePayments.Checked)
                Permissions += (int)clsUser.enPermissions.eManagePayments;
            if (chkShowMedicalRecords.Checked)
                Permissions += (int)clsUser.enPermissions.eShowMedicalRecords;
            if (chkAddEditDoctors.Checked)
                Permissions += (int)clsUser.enPermissions.eAddEditDoctors;
            if (chkManageUsers.Checked)
                Permissions += (int)clsUser.enPermissions.eManageUsers;

            return Permissions;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Please check red circle(s).", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _User.Username = ctrlRequiredTextBoxUsername.Text.Trim();
            if(_Mode == enMode.AddNew)
                _User.Password = ctrlRequiredTextBoxConfirmPassword.Text.Trim();
            _User.Role = Convert.ToByte(cbRole.SelectedIndex + 1);
            _User.Permissions = (clsUser.enPermissions)_GetSelectedPermiessions();

            if (_User.Save())
            {
                MessageBox.Show("User Information is saved successfully.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                this._Mode = enMode.Update;
                this.Text = "Update";
                lblMode.Text = "Update User";
                lblUserID.Text = _User.UserID.ToString();
            }
            else
                MessageBox.Show("An error occurred during information save.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ctrlRequiredTextBoxConfirmPassword_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(ctrlRequiredTextBoxNewPassword.Text.Trim() != 
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

            if(clsUser.IsUsernameUsed(ctrlRequiredTextBoxUsername.Text.Trim()))
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

        private void _LoadDefaultAdminPermissions()
        {
            chkAll.Checked = true;
            chkAll.Enabled = true;

            // Disable and uncheck all by default
            foreach (CheckBox chk in _Checkboxes)
            {
                chk.Enabled = false;
                chk.Checked = false;
            }
        }

        private void _LoadDefaultReceptionistPermissons()
        {
            chkAll.Enabled = false;

            foreach (CheckBox chk in _Checkboxes)
            {
                chk.Enabled = true;
                chk.Checked = false; // Reset permissions to ensure consistency
            }

            chkManagePatients.Checked = true;
            chkManageAppointments.Checked = true;
            chkManagePayments.Checked = true;
            chkShowMedicalRecords.Checked = true;
            chkAddEditMedicalRecords.Enabled = false;
            chkAddEditDoctors.Enabled = false;
            chkManageUsers.Enabled = false;
        }

        private void _LoadDefaultDoctorPermissions()
        {
            chkAll.Checked = false;
            chkAll.Enabled = false;

            foreach (CheckBox chk in _Checkboxes)
                chk.Enabled = false;

            chkManagePatients.Checked = false;

            chkManageAppointments.Checked = true;
            chkManagePayments.Checked = false;
            chkShowMedicalRecords.Checked = true;
            chkAddEditMedicalRecords.Checked = true;
        }

        private void cbRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbRole.SelectedIndex == 0) // Admin
                _LoadDefaultAdminPermissions();

            if (cbRole.SelectedIndex == 1) // Receptionist
                _LoadDefaultReceptionistPermissons();

            if (cbRole.SelectedIndex == 2) // Doctor
                _LoadDefaultDoctorPermissions();
        }
    }
}