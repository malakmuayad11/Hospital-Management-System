using Hospital_Business;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Hospital_System.Appointments
{
    public partial class frmAddEditAppointment : Form
    {
        private clsAppointment _Appointment;
        private clsDoctor _Doctor;

        public enum enMode { AddNew = 1, Update = 2 } // Update mode is for rescheduling the appointment.
        private enMode _Mode;
        public frmAddEditAppointment()
        {
            InitializeComponent();
            _Appointment = new clsAppointment();
            this._Mode = enMode.AddNew;
            this.Text = "Add New Appointment";
        }

        public frmAddEditAppointment(int AppointmentID)
        {
            InitializeComponent();
            _Appointment = clsAppointment.Find(AppointmentID);
            this._Mode = enMode.Update;
            _LoadUpdateMode();
            this.Text = "Reschedule Appointment";
        }
        
        private void _LoadUpdateMode()
        {
            tabControl1.SelectedTab = tpAppointmentInfo;
            tpPatientInfo.Enabled = false;
            tpDoctorInfo.Enabled = false;
            lblAppointmentID.Text = _Appointment.AppointmentID.ToString();
            txtReasonForVisit.Text = _Appointment.ReasonForVisit;
            dtpAppointmentDate.Value = _Appointment.AppointmentDate;
            dtpAppointmentTime.Value = new DateTime(_Appointment.AppointmentDate.Year, _Appointment.AppointmentDate.Month,
                _Appointment.AppointmentDate.Day, _Appointment.AppointmentTime.Hours,
                _Appointment.AppointmentTime.Minutes, _Appointment.AppointmentTime.Seconds);
            lblStatus.Text = _Appointment.StatusString;
            _Doctor = clsDoctor.Find(_Appointment.DoctorID);
        }

        private void ctrlPatientInfoWithFilter1_FindPatient(object sender, bool IsPatientFound)
        {
            if(!IsPatientFound)
            {
                tpDoctorInfo.Enabled = false;
                tpAppointmentInfo.Enabled = false;
            }
            else
            {
                tpDoctorInfo.Enabled = true;
                tpAppointmentInfo.Enabled = true;
            }
        }

        private void ctrlDoctorInfoWithFilter1_FindDoctor(object sender, bool IsDoctorFound)
        {
            if (!IsDoctorFound)
                tpAppointmentInfo.Enabled = false;
            else
            {
                tpAppointmentInfo.Enabled = true;
                _Doctor = clsDoctor.Find(ctrlDoctorInfoWithFilter1.DoctorID);
            }
        }

        private void _LoadData()
        {
            if (this._Mode == enMode.AddNew)
            {
                _Appointment.DoctorID = ctrlDoctorInfoWithFilter1.DoctorID;
                _Appointment.PatientID = ctrlPatientInfoWithFilter1.PatientID;
                _Appointment.ReasonForVisit = txtReasonForVisit.Text.Trim();
            }
            _Appointment.AppointmentDate = dtpAppointmentDate.Value;
            _Appointment.AppointmentTime = new TimeSpan(
                dtpAppointmentTime.Value.Hour,
                dtpAppointmentTime.Value.Minute,
                dtpAppointmentTime.Value.Second);
        }

        private void _HandleSaveMode()
        {
            lblAppointmentID.Text = _Appointment.AppointmentID.ToString();
            lblStatus.Text = _Appointment.StatusString;
            dtpAppointmentDate.Enabled = false;
            dtpAppointmentTime.Enabled = false;
            txtReasonForVisit.Enabled = false;
            lblStatus.Text = _Mode == enMode.AddNew ? "Scheduled" : "Resheduled";
            btnSave.Enabled = false;
            tpDoctorInfo.Enabled = false;
            tpPatientInfo.Enabled = false;
        }

        private void _SaveAppointment()
        {
            if(clsPatient.HasPatientAppointmentAt(_Appointment.PatientID, _Appointment.AppointmentDate, _Appointment.AppointmentTime))
            {
                MessageBox.Show("Patient already has another appointment at the same time.", "Not Allowed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (_Appointment.Save())
            {
                if (_Mode == enMode.AddNew)
                    MessageBox.Show("Appointment is scheduled successfully", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Appointment is rescheduled successfully", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                _HandleSaveMode();
            }
            else
                MessageBox.Show("Doctor already has an appointment at this time, choose another time", "Not Allowed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(!this.ValidateChildren())
            {
                MessageBox.Show("Please check red circles", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _LoadData();
            _SaveAppointment();
        }

        private void btnClose_Click(object sender, EventArgs e) => this.Close();

        private void dtpAppointmentDate_Validating(object sender, CancelEventArgs e)
        {
            if (_Doctor != null)
            {
                byte SelectedDay = Convert.ToByte(dtpAppointmentDate.Value.DayOfWeek);

                if (!_Doctor.DoesDoctorWorkOn(SelectedDay))
                {
                    e.Cancel = true;
                    dtpAppointmentDate.Focus();
                    errorProvider1.SetError(dtpAppointmentDate, $"Doctor does not work on {dtpAppointmentDate.Value.ToString("dddd")}");
                }
                else
                {
                    e.Cancel = false;
                    errorProvider1.SetError(dtpAppointmentDate, string.Empty);
                }
            }
        }

        private void dtpAppointmentTime_Validating(object sender, CancelEventArgs e)
        {
            if (_Doctor != null)
            {
                TimeSpan SelectedTime = new TimeSpan(dtpAppointmentTime.Value.Hour, dtpAppointmentTime.Value.Minute,
                    dtpAppointmentTime.Value.Second);

                if(!(SelectedTime >= _Doctor.StartWorkHour && SelectedTime <= _Doctor.EndWorkHour))
                {
                    e.Cancel = true;
                    dtpAppointmentDate.Focus();
                    errorProvider1.SetError(dtpAppointmentTime, "Doctor does not work at this time.");
                }
                else
                {
                    e.Cancel = false;
                    errorProvider1.SetError(dtpAppointmentTime, string.Empty);
                }
            }
        }

        private void txtReasonForVisit_Validating(object sender, CancelEventArgs e) =>
            clsValidation.ValidateRequiredTextBox(sender, this.errorProvider1, e);

        private void frmAddEditAppointment_Load(object sender, EventArgs e)
        {
            if (_Mode == enMode.AddNew)
            {
                dtpAppointmentDate.MinDate = DateTime.Now;
                dtpAppointmentTime.MinDate = DateTime.Now;
            }
        }
    }
}
