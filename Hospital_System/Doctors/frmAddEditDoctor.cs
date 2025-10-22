using Hospital_Business;
using System;
using System.ComponentModel;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Hospital_System
{
    public partial class frmAddEditDoctor : Form
    {
        private clsDoctor _Doctor;
        private int _DoctorID;
        private enum enMode { AddNew = 1, Update = 2 };
        private enMode _Mode;

        public event EventHandler<clsDoctor> DoctorAdded;

        public void OnDoctorAdded(clsDoctor Doctor) => DoctorAdded?.Invoke(this, Doctor);

        public frmAddEditDoctor()
        {
            InitializeComponent();
            _Doctor = new clsDoctor();
            _Mode = enMode.AddNew;
        }

        public frmAddEditDoctor(int DoctorID)
        {
            InitializeComponent();
            _Doctor = clsDoctor.Find(DoctorID);
            _DoctorID = DoctorID;
            _Mode = enMode.Update;
        }

        private async Task _LoadCb()
        {
            this.cbSpecialty.Items.Clear();
            DataTable Specialities = await clsConsultation.GetAllSpecialities();
            foreach (DataRow Speciality in Specialities.Rows)
                this.cbSpecialty.Items.Add(Speciality["Specialty"]);   
        }

        private void _LoadAddNewMode()
        {
            this.Text = "Add New Doctor";
            lblMode.Text = "Add New Doctor";
            cbEndWorkDay.SelectedIndex = 4;
            cbStartWorkDay.SelectedIndex = 0;
        }

        private void _LoadUpdateMode()
        {
            this.Text = "Update Doctor";
            lblMode.Text = "Update Doctor";
            lblDoctorID.Text = _Doctor.DoctorID.ToString();
            ctrlRequiredTextBoxFirstName.Text = _Doctor.FirstName;
            ctrlRequiredTextBoxLastName.Text = _Doctor.LastName;
            mtxtPhone.Text = _Doctor.Phone;
            txtEmail.Text = _Doctor.Email;
            cbStartWorkDay.SelectedIndex = _Doctor.StartWorkDay;
            cbEndWorkDay.SelectedIndex = _Doctor.EndWorkDay;
            dtpStartWorkHour.Value = DateTime.Now.Date + _Doctor.StartWorkHour;
            dtpEndWorkHour.Value = DateTime.Now.Date + _Doctor.EndWorkHour;
            cbSpecialty.SelectedIndex = _Doctor.ConsultationID - 2;  // -2 to ensure consistency with data in the database
            cbGender.SelectedIndex = _Doctor.Gender;
        }

        private void mtxtPhone_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(mtxtPhone.Text) || !mtxtPhone.MaskCompleted)
            {
                e.Cancel = true;
                mtxtPhone.Focus();
                errorProvider1.SetError(mtxtPhone, "This field is required");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(mtxtPhone, string.Empty);
            }
        }

        private void btnClose_Click(object sender, EventArgs e) => this.Close();

        private async void frmAddEditDoctor_Load(object sender, EventArgs e)
        {
            cbGender.SelectedIndex = 0;
            await _LoadCb();
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

        private bool _SaveInfo()
        {
            _Doctor.FirstName = ctrlRequiredTextBoxFirstName.Text.Trim();
            _Doctor.LastName = ctrlRequiredTextBoxLastName.Text.Trim();
            _Doctor.Phone = mtxtPhone.Text.Trim();
            _Doctor.Email = txtEmail.Text.Trim();
            _Doctor.StartWorkDay = Convert.ToByte(cbStartWorkDay.SelectedIndex);
            _Doctor.EndWorkDay = Convert.ToByte(cbEndWorkDay.SelectedIndex);
            _Doctor.StartWorkHour = dtpStartWorkHour.Value.TimeOfDay;
            _Doctor.EndWorkHour = dtpEndWorkHour.Value.TimeOfDay;
            _Doctor.ConsultationID = cbSpecialty.SelectedIndex + 2;
            _Doctor.Gender = (byte)cbGender.SelectedIndex;
            return _Doctor.Save();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_SaveInfo())
            {
                MessageBox.Show("Doctor Information is saved successfully.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                OnDoctorAdded(_Doctor);
                lblDoctorID.Text = _Doctor.DoctorID.ToString();
                this._Mode = enMode.Update;
                this.Text = "Update Doctor";
                lblMode.Text = "Update Doctor";
            }
            else
                MessageBox.Show("An error occurred during information save.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtEmail.Text.Trim()))
                return;

            if (!clsValidation.ValidateEmail(txtEmail.Text.Trim()))
            {
                e.Cancel = true;
                txtEmail.Focus();
                errorProvider1.SetError(txtEmail, "Enter a valid email.");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtEmail, string.Empty);
            }
        }
    }
}
