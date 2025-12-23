using Hospital_Business;
using System;
using System.Windows.Forms;

namespace Hospital_System.Doctors
{
    public partial class ctrlDoctorInfo : UserControl
    {
        private clsDoctor _Doctor;
        public ctrlDoctorInfo() => InitializeComponent();

        public int DoctorID { get => Convert.ToInt32(lblDoctorID.Text.Trim()); }
        public string DoctorName { get => lblFirstName.Text + " " + lblLastName.Text; }

        public class DoctorEventArgs : EventArgs
        {
            public bool IsFound { get; set; }
            public DoctorEventArgs(bool IsFound)
            {
                this.IsFound = IsFound;
            }
        }

        public event EventHandler<DoctorEventArgs> OnDoctorLoad;

        protected virtual void DoctorLoad(DoctorEventArgs e) => OnDoctorLoad?.Invoke(this, e);

        public void DoctorLoad(bool IsFound) => DoctorLoad(new DoctorEventArgs(IsFound));

        private void _LoadData()
        {
            lblDoctorID.Text = _Doctor.DoctorID.ToString();
            lblFirstName.Text = _Doctor.FirstName;
            lblLastName.Text = _Doctor.LastName;
            lblPhone.Text = _Doctor.Phone;
            lblEmail.Text = string.IsNullOrEmpty(_Doctor.Email) ? "Not Provided" : _Doctor.Email;
            lblStartWorkDay.Text = _Doctor.GetDayName(_Doctor.StartWorkDay);
            lblEndWorkDay.Text = _Doctor.GetDayName(_Doctor.EndWorkDay);
            lblStartWorkHour.Text = _Doctor.StartWorkHour.ToString("hh");
            lblEndWorkHour.Text = _Doctor.EndWorkHour.ToString("hh");
            lblSpecialty.Text = _Doctor.ConsultationInfo.Specialty;
        }

        public void LoadDoctorInfo(int DoctorID)
        {
            _Doctor = clsDoctor.Find(DoctorID);

            if (_Doctor == null)
            {
                MessageBox.Show("An error occurred while loading data.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                DoctorLoad(false);
                return;
            }
            _LoadData();
            DoctorLoad(true);
        }
    }
}