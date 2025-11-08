using Hospital_Business;
using Hospital_System.Doctors;
using Hospital_System.Main;
using Hospital_System.MedicalRecords;
using System;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hospital_System
{
    public partial class frmDoctorMainScreen : frmManageScreenNoAddButton
    {
        private DataTable _dtTodaysAppointments;
        private clsDoctor _Doctor;
        private Form _LoginForm;
        public frmDoctorMainScreen(Form LoginForm, int UserID)
        {
            InitializeComponent();
            _LoginForm = LoginForm;
            _Doctor = clsDoctor.FindByUserID(UserID);
            base.BTN.Visible = false;
        }

        private async Task _LoadDataAsync()
        {
            _dtTodaysAppointments = await clsDoctor.GetTodaysAppointmentsForDoctor(_Doctor.DoctorID);
            base.DGV.DataSource = _dtTodaysAppointments;
        }

        private async Task _RefreshAsync()
        {
            await _LoadDataAsync();

            if (base.DGV.Columns.Count > 0)
            {
                base.DGV.Columns[0].Width = 110;
                base.DGV.Columns[0].HeaderText = "Appointment ID";

                base.DGV.Columns[1].Width = 110;
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

        private async void frmDoctorMainScreen_Load(object sender, EventArgs e)
        {
            if(_Doctor == null)
            {
                MessageBox.Show("An error occurred while loading", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                _LoginForm.Visible = true;
                return;
                
            }

            await _LoadDataAsync();
            base.DGV.ContextMenuStrip = base.CMS;
            await _RefreshAsync();
            lblAppointments.Text = clsDoctor.GetAppointmentsCountForDoctor(_Doctor.DoctorID).ToString();
            lblMedicalRecords.Text = clsDoctor.GetMedicalRecordsCountForDoctor(_Doctor.DoctorID).ToString();
            lblPatients.Text = clsDoctor.GetPatientsCountForDoctor(_Doctor.DoctorID).ToString();
        }

        private void btn_MouseEnter(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.BackColor = Color.White;
            btn.ForeColor = Color.SteelBlue;
        }

        private void btn_MouseLeave(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.BackColor = Color.SteelBlue;
            btn.ForeColor = Color.White;
        }

        private void btnAppointments_Click(object sender, EventArgs e)
        {
            frmTodaysAppointmentsForDoctor frm = new
                frmTodaysAppointmentsForDoctor(_Doctor.DoctorID);
            frm.ShowDialog();
            lblMedicalRecords.Text = clsDoctor.GetMedicalRecordsCountForDoctor(_Doctor.DoctorID).ToString();
            frmDoctorMainScreen_Load(null, null); // refresh if changes in appointments are done
        }

        private void btnMedicalRecords_Click(object sender, EventArgs e)
        {
            frmManageMedicalRecords frm = new frmManageMedicalRecords();
            frm.ShowDialog();
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            this.Close();
            _LoginForm.ShowDialog();
        }

        private void showProfileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserInfo frm = new frmUserInfo(_Doctor.UserID);
            frm.ShowDialog();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangePassword frm = new frmChangePassword(_Doctor.UserID);
            frm.ShowDialog();
        }
    }
}
