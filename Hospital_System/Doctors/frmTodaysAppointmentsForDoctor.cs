using Hospital_Business;
using Hospital_System.Appointments;
using Hospital_System.MedicalRecords;
using Hospital_System.Patients;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using Hospital_System.Properties;

namespace Hospital_System.Doctors
{
    public partial class frmTodaysAppointmentsForDoctor : frmManageScreen
    {
        private DataTable _dtTodaysDoctorAppointments;
        private int _DoctorID;
        public frmTodaysAppointmentsForDoctor(int DoctorID)
        {
            InitializeComponent();
            this.Size = base.Size;
            _DoctorID = DoctorID; 
        }

        private async Task _LoadDataAsync()
        {
            _dtTodaysDoctorAppointments = await clsDoctor.GetTodaysAppointmentsForDoctor(_DoctorID);
            base.DGV.DataSource = _dtTodaysDoctorAppointments;
        }

        private void _LoadCMS()
        {
            CMS.Opened += CMS_Opened;
            base.CMS.Items.Add("Show Appointment Info");
            base.CMS.Items[0].Image = Resources.Manage_Appointments;
            base.CMS.Items.Add("Show Patient Info");
            base.CMS.Items[1].Image = Resources.User_32__2;
            base.CMS.Items.Add("Add Medical Record");
            base.CMS.Items[2].Image = Resources.add;
            base.CMS.Items.Add("Mark Appointment as Completed");
            base.CMS.Items[3].Image = Resources.Manage_Appointments;
            CMS.ItemClicked += CMS_ItemClicked;
            BTN.Visible = false;
        }

        private void CMS_Opened(object sender, EventArgs e)
        {
            if (base.DGV.Rows.Count > 0)
            {
                CMS.Items[2].Enabled =  //Add Medical Record
                    !(clsAppointment.HasAppointmentMedicalRecord(Convert.ToInt32(DGV.CurrentRow.Cells[0].Value)));

                CMS.Items[3].Enabled = (DGV.CurrentRow.Cells[5].Value.ToString() != "Completed");
            }
        }

        private async Task _CompleteAppointment()
        {
            if(MessageBox.Show("Are you sure you want to mark this appointment as completed?", 
                "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (clsAppointment.UpdateAppointmentStatus((int)base.DGV.CurrentRow.Cells[0].Value, 4))
                    await _RefreshAsync();
                else
                    MessageBox.Show("An error occurred and the appointment is not completed", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void CMS_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "Show Appointment Info":
                    {
                        frmAppointmentInfo frm = new frmAppointmentInfo((int)base.DGV.CurrentRow.Cells[0].Value);
                        frm.ShowDialog();
                    }
                    break;
                case "Show Patient Info":
                    {
                        frmPatientInfo frm = new frmPatientInfo((int)base.DGV.CurrentRow.Cells[1].Value);
                        frm.ShowDialog();
                        await _RefreshAsync();
                    }
                    break;
                case "Add Medical Record":
                    {
                        frmAddEditMedicalRecord frm = new frmAddEditMedicalRecord(
                            (int)base.DGV.CurrentRow.Cells[0].Value);
                        frm.ShowDialog();
                    }
                    break;
                case "Mark Appointment as Completed":
                        await _CompleteAppointment();
                    break;
            }
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
                base.DGV.Columns[1].HeaderText = "Patient ID";

                base.DGV.Columns[2].Width = 120;
                base.DGV.Columns[2].HeaderText = "Appointment Date";

                base.DGV.Columns[3].Width = 120;
                base.DGV.Columns[3].HeaderText = "Appointment Time";

                base.DGV.Columns[4].Width = 120;
                base.DGV.Columns[4].HeaderText = "Reason for Visit";

                base.DGV.Columns[5].Width = 110;
                base.DGV.Columns[5].HeaderText = "Status";
            }
            base.RecordsNumber = base.DGV.Rows.Count.ToString();
        }

        private async void frmTodaysAppointmentsForDoctor_Load(object sender, EventArgs e)
        {
            base.Image = Resources.Manage_Appointments;
            await _LoadDataAsync();
            _LoadCMS();
            base.DGV.ContextMenuStrip = base.CMS;
            await _RefreshAsync();
        }
    }
}
