using Hospital_Business;
using Hospital_System.Appointments;
using Hospital_System.Billings;
using Hospital_System.Consultations;
using Hospital_System.MedicalRecords;
using Hospital_System.Patients;
using System;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Hospital_System
{
    public partial class frmMainScreen : Form
    {
        private DataTable _dtTodaysAppointments;
        private Form _LoginForm;
        public frmMainScreen(Form LoginForm)
        {
            InitializeComponent();
            _LoginForm = LoginForm;
        }

        private async Task _LoadDataAsync()
        {
            _dtTodaysAppointments = await clsAppointment.GetTodaysAppointments();
            lblUsers.Text = clsUser.GetUsersCount().ToString();
            lblDoctors.Text = clsDoctor.GetDocotrsCount().ToString();
            lblAppointments.Text = clsAppointment.GetAppointmentsCount().ToString();
            dgvTodaysAppointments.DataSource = _dtTodaysAppointments;

            if (dgvTodaysAppointments.Rows.Count > 0)
            {
                dgvTodaysAppointments.Columns[0].Width = 110;
                dgvTodaysAppointments.Columns[0].HeaderText = "Appointment ID";

                dgvTodaysAppointments.Columns[1].Width = 150;
                dgvTodaysAppointments.Columns[1].HeaderText = "Patient Name";

                dgvTodaysAppointments.Columns[2].Width = 110;
                dgvTodaysAppointments.Columns[2].HeaderText = "Patient National No.";

                dgvTodaysAppointments.Columns[3].Width = 150;
                dgvTodaysAppointments.Columns[3].HeaderText = "Doctor Name";

                dgvTodaysAppointments.Columns[4].Width = 150;
                dgvTodaysAppointments.Columns[4].HeaderText = "Appointment Date";

                dgvTodaysAppointments.Columns[5].Width = 150;
                dgvTodaysAppointments.Columns[5].HeaderText = "Appointment Time";

                dgvTodaysAppointments.Columns[6].Width = 150;
                dgvTodaysAppointments.Columns[6].HeaderText = "Status";
            }
            lblRecords.Text = dgvTodaysAppointments.Rows.Count.ToString();
        }

        private async void frmMainScreen_Load(object sender, EventArgs e) => await _LoadDataAsync();

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

        private void btnUsers_Click(object sender, EventArgs e)
        {
            frmManageUsers frm = new frmManageUsers();
            frm.ShowDialog();
            frmMainScreen_Load(null, null);
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            _LoginForm.ShowDialog();
        }

        private void btnDoctors_Click(object sender, EventArgs e)
        {
            frmManageDoctors frm = new frmManageDoctors();
            frm.ShowDialog();
            frmMainScreen_Load(null, null);
        }

        private void btnPatients_Click(object sender, EventArgs e)
        {
            if (clsValidation.DoesCurrentUserHavePermission(clsUser.enPermissions.eManagePatients))
            {
                frmManagePatients frm = new frmManagePatients();
                frm.ShowDialog();
            }
        }

        private void btnAppointments_Click(object sender, EventArgs e)
        {
            if (clsValidation.DoesCurrentUserHavePermission(clsUser.enPermissions.eManageAppointments))
            {
                frmManageAppointments frm = new frmManageAppointments();
                frm.ShowDialog();
                frmMainScreen_Load(null, null); // need to refresh appointments if a new appointment is added for today
            }
        }

        private void btnPayments_Click(object sender, EventArgs e)
        {
            if (clsValidation.DoesCurrentUserHavePermission(clsUser.enPermissions.eManagePayments))
            {
                frmManageBillings frm = new frmManageBillings();
                frm.ShowDialog();
                frmMainScreen_Load(null, null);// refresh payment status, if paid
            }
        }

        private void btnConsultations_Click(object sender, EventArgs e)
        {
            frmManageConsultations frm = new frmManageConsultations();
            frm.ShowDialog();
        }

        private void btnMedicalRecords_Click(object sender, EventArgs e)
        {
            if (clsValidation.DoesCurrentUserHavePermission(clsUser.enPermissions.eShowMedicalRecords))
            {
                frmManageMedicalRecords frm = new frmManageMedicalRecords();
                frm.ShowDialog();
            }
        }

        private void showProfileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserInfo frm = new frmUserInfo(clsGlobal.CurrentUser.UserID);
            frm.ShowDialog();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangePassword frm = new frmChangePassword(clsGlobal.CurrentUser.UserID);
            frm.ShowDialog();
        }
    }
}
