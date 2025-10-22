using Hospital_Business;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using Hospital_System.Properties;

namespace Hospital_System.Patients
{
    public partial class frmManagePatients : frmManageScreen
    {
        private DataTable _dtAllPatients;
        public frmManagePatients() => InitializeComponent();

        private async Task _LoadDataAsync()
        {
            _dtAllPatients = await clsPatient.GetAllPatientsAysnc();
            base.DGV.DataSource = _dtAllPatients;
        }

        private void _LoadCMS()
        {
            base.CMS.Items.Add("Show Info");
            base.CMS.Items[0].Image = Resources.User_32__2;
            base.CMS.Items.Add("Edit");
            base.CMS.Items[1].Image = Resources.rescheduling;
            base.CMS.Items.Add("Show Medical Records History");
            base.CMS.Items[2].Image = Resources.Manage_Consultations;
            base.CMS.Items.Add("Show Prescriptions History");
            base.CMS.Items[3].Image = Resources.Manage_Consultations;
            CMS.ItemClicked += CMS_ItemClicked;
            BTN.Click += BTN_Click;
            CMS.Opened += CMS_Opened;
        }

        private void CMS_Opened(object sender, EventArgs e)
        {
            int PatientID = (int)DGV.CurrentRow.Cells[0].Value;
            CMS.Items[2].Enabled = clsPatient.HasPatientMedicalRecords(PatientID); //Show Medical Records History
            CMS.Items[3].Enabled = clsPatient.HasPatientPrescriptions(PatientID); //Show Prescriptions History
        }

        private async void BTN_Click(object sender, EventArgs e)
        {
            frmAddEditPatient frm = new frmAddEditPatient();
            frm.ShowDialog();
            await _RefreshAsync();
        }

        private async void CMS_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "Show Info":
                    {
                        frmPatientInfo frm =
                        new frmPatientInfo((int)base.DGV.CurrentRow.Cells[0].Value);
                        frm.ShowDialog();
                    }
                    break;
                case "Edit":
                    {
                        frmAddEditPatient frm = new frmAddEditPatient((int)base.DGV.CurrentRow.Cells[0].Value);
                        frm.ShowDialog();
                        await _RefreshAsync();
                    }
                    break;
                case "Show Medical Records History":
                    {
                        frmPatientHistory frm = new frmPatientHistory((int)base.DGV.CurrentRow.Cells[0].Value, true);
                        frm.ShowDialog();
                    }
                    break;
                case "Show Prescriptions History":
                    {
                        frmPatientHistory frm = new frmPatientHistory((int)base.DGV.CurrentRow.Cells[0].Value, false);
                        frm.ShowDialog();
                    }
                    break;
            }
        }

        private async Task _RefreshAsync()
        {
            await _LoadDataAsync();

            if (base.DGV.Columns.Count > 0)
            {
                base.DGV.Columns[0].Width = 110;
                base.DGV.Columns[0].HeaderText = "Patient ID";

                base.DGV.Columns[1].Width = 120;
                base.DGV.Columns[1].HeaderText = "Name";

                base.DGV.Columns[2].Width = 120;
                base.DGV.Columns[2].HeaderText = "National No";

                base.DGV.Columns[3].Width = 120;
                base.DGV.Columns[3].HeaderText = "Date of Birth";

                base.DGV.Columns[4].Width = 120;
                base.DGV.Columns[4].HeaderText = "Gender";

                base.DGV.Columns[5].Width = 120;
                base.DGV.Columns[5].HeaderText = "Emergency Contact";
            }
            base.RecordsNumber = base.DGV.Rows.Count.ToString();
        }

        private async void frmManagePatients_Load(object sender, EventArgs e)
        {
            base.Image = Resources.Manage_People;
            await _LoadDataAsync();
            _LoadCMS();
            base.DGV.ContextMenuStrip = base.CMS;
            await _RefreshAsync();
        }
    }
}
