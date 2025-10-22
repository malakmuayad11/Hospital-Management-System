using Hospital_Business;
using Hospital_System.Doctors;
using Hospital_System.MedicalRecords;
using Hospital_System.Patients;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using Hospital_System.Properties;

namespace Hospital_System.Appointments
{
    public partial class frmManageAppointments : frmManageScreen
    {
        private DataTable _dtAllAppointments;
        public frmManageAppointments() => InitializeComponent();

        private async Task _LoadDataAsync()
        {
            _dtAllAppointments = await clsAppointment.GetAllAppointments();
            base.DGV.DataSource = _dtAllAppointments;
        }

        private void _LoadCMS()
        {
            base.CMS.Opened += CMS_Opened;
            base.CMS.Items.Add("Show Details");
            base.CMS.Items[0].Image = Resources.Manage_Appointments;
            base.CMS.Items.Add("-");
            base.CMS.Items.Add("Reschedule");
            base.CMS.Items[2].Image = Resources.rescheduling;
            base.CMS.Items.Add("Cancel");
            base.CMS.Items[3].Image = Resources.cross_32;
            base.CMS.Items.Add("Doctor Information");
            base.CMS.Items[4].Image = Resources.Manage_Doctors;
            base.CMS.Items.Add("Patient Information");
            base.CMS.Items[5].Image = Resources.User_32__2;
            base.CMS.Items.Add("Medical Record Information");
            base.CMS.Items[6].Image = Resources.Manage_Appointments;
            CMS.ItemClicked += CMS_ItemClicked;
            BTN.Click += BTN_Click;
        }

        private void CMS_Opened(object sender, EventArgs e)
        {
            string Status = (string)base.DGV.CurrentRow.Cells[5].Value;

            if(Status.ToLower() == "cancelled" || Status.ToLower() == "completed")
            {
                CMS.Items[2].Enabled = false;
                CMS.Items[3].Enabled = false;
            }
            else
            {
                CMS.Items[2].Enabled = true;
                CMS.Items[3].Enabled = true;
            }

            CMS.Items[6].Enabled = clsAppointment.HasAppointmentMedicalRecord((int)base.DGV.CurrentRow.Cells[0].Value);
        }

        private async Task _CancelAppointment()
        {
            if (clsAppointment.CancelAppointment((int)base.DGV.CurrentRow.Cells[0].Value))
            {
                MessageBox.Show("Appointment is cancelled successfully", "Cancelled",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                await _RefreshAsync();
            }
            else
                MessageBox.Show("An error occurred while cancelling the appointment", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private async void CMS_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            clsAppointment appointment = clsAppointment.Find((int)base.DGV.CurrentRow.Cells[0].Value);
            switch (e.ClickedItem.Text)
            {
                case "Show Details":
                    {
                        frmAppointmentInfo frm = new frmAppointmentInfo((int)base.DGV.CurrentRow.Cells[0].Value);
                        frm.ShowDialog();
                    }
                    break;
                case "Reschedule":
                    {
                        frmAddEditAppointment frm = new frmAddEditAppointment((int)base.DGV.CurrentRow.Cells[0].Value);
                        
                        frm.ShowDialog();
                        await _RefreshAsync();
                        break;
                    }
                case "Cancel":
                    {
                        if (MessageBox.Show("Are you sure you want to cancel this appointment?", "Confirm",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            await _CancelAppointment();
                        break;
                    }
                case "Doctor Information":
                    {
                        frmDoctorInfo frm =
                        new frmDoctorInfo(appointment.DoctorID);
                        frm.ShowDialog();
                        break;
                    }
                case "Patient Information":
                    {
                        frmPatientInfo frm = new frmPatientInfo(appointment.PatientID);
                        frm.ShowDialog();
                        break;
                    }
                case "Medical Record Information":
                    {
                        clsMedicalRecord _MedicalRecord = clsMedicalRecord.FindByAppointmentID((int)base.DGV.CurrentRow.Cells[0].Value);
                        frmMedicalRecordInfo frm = new frmMedicalRecordInfo(_MedicalRecord.MedicalRecordID);
                        frm.ShowDialog();
                        break;
                    }
            }
        }

        private async void BTN_Click(object sender, EventArgs e)
        {
            frmAddEditAppointment frm = new frmAddEditAppointment();
            frm.ShowDialog();
            await _RefreshAsync();
        }

        private async void frmManageAppointments_Load(object sender, EventArgs e)
        {
            base.Image = Resources.Manage_Appointments;
            await _LoadDataAsync();
            _LoadCMS();
            base.DGV.ContextMenuStrip = base.CMS;
            await _RefreshAsync();
        }

        private async Task _RefreshAsync()
        {
            base.Image = Resources.Manage_Appointments;
            await _LoadDataAsync();

            if (base.DGV.Rows.Count > 0)
            {
                base.DGV.Columns[0].Width = 110;
                base.DGV.Columns[0].HeaderText = "Appointment ID";

                base.DGV.Columns[1].Width = 120;
                base.DGV.Columns[1].HeaderText = "Patient Name";

                base.DGV.Columns[2].Width = 120;
                base.DGV.Columns[2].HeaderText = "NationalNo";

                base.DGV.Columns[3].Width = 120;
                base.DGV.Columns[3].HeaderText = "Doctor Name";

                base.DGV.Columns[4].Width = 120;
                base.DGV.Columns[4].HeaderText = "Appointment Date";

                base.DGV.Columns[5].Width = 120;
                base.DGV.Columns[5].HeaderText = "Status";
            }
            base.RecordsNumber = base.DGV.Rows.Count.ToString();
        }
    }
}
