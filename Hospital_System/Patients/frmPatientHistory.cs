using Hospital_Business;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hospital_System.Patients
{
    public partial class frmPatientHistory : Form
    {
        private int _PatientID;
        private enum enMode { LoadMedicalRecords = 1, LoadPrescriptions = 2 }
        private enMode _Mode;
        public frmPatientHistory(int PatientID, bool IsMedicalRecord)
        {
            InitializeComponent();
            _PatientID = PatientID;
            _Mode = IsMedicalRecord ? enMode.LoadMedicalRecords : enMode.LoadPrescriptions;
        }

        private async Task _LoadDataAsync()
        {
            ctrlPatientInfo1.LoadPatientInfo(_PatientID);
            if (_Mode == enMode.LoadMedicalRecords)
            {
                dgvHistoryRecords.DataSource = await clsPatient.GetPatientHistoryAsync(_PatientID, true);
                this.Text = "Medical Records History";
            }
            else
            {
                dgvHistoryRecords.DataSource = await clsPatient.GetPatientHistoryAsync(_PatientID, false);
                this.Text = "Prescriptions History";
            }
            _LoadDGV();
        }

        private void _LoadMedicalRecords()
        {
            if (dgvHistoryRecords.Columns.Count > 0)
            {
                dgvHistoryRecords.Columns[1].Width = 120;
                dgvHistoryRecords.Columns[1].HeaderText = "Medical Record ID";

                dgvHistoryRecords.Columns[2].Width = 110;
                dgvHistoryRecords.Columns[2].HeaderText = "Appointment ID";

                dgvHistoryRecords.Columns[3].Width = 120;
                dgvHistoryRecords.Columns[3].HeaderText = "Symptoms";

                dgvHistoryRecords.Columns[4].Width = 120;
                dgvHistoryRecords.Columns[4].HeaderText = "Diagnosis";

                dgvHistoryRecords.Columns[5].Width = 140;
                dgvHistoryRecords.Columns[5].HeaderText = "Medical Record Notes";
            }
        }

        private void _LoadPrescriptions()
        {
            if (dgvHistoryRecords.Columns.Count > 0)
            {
                dgvHistoryRecords.Columns[1].Width = 120;
                dgvHistoryRecords.Columns[1].HeaderText = "Prescription ID";

                dgvHistoryRecords.Columns[2].Width = 110;
                dgvHistoryRecords.Columns[2].HeaderText = "Appointment ID";

                dgvHistoryRecords.Columns[3].Width = 120;
                dgvHistoryRecords.Columns[3].HeaderText = "Medication Name";

                dgvHistoryRecords.Columns[4].Width = 120;
                dgvHistoryRecords.Columns[4].HeaderText = "Dosage";

                dgvHistoryRecords.Columns[5].Width = 130;
                dgvHistoryRecords.Columns[5].HeaderText = "Duration in Days";

                dgvHistoryRecords.Columns[6].Width = 130;
                dgvHistoryRecords.Columns[6].HeaderText = "Duration in Months";
            }
        }

        private void _LoadDGV()
        {
            if (dgvHistoryRecords.Columns.Count > 0)
            {
                dgvHistoryRecords.Columns[0].Width = 110;
                dgvHistoryRecords.Columns[0].HeaderText = "Patient ID";

                if (_Mode == enMode.LoadMedicalRecords)
                    _LoadMedicalRecords();
                else
                    _LoadPrescriptions();
            }
        }

        private async void frmPatientHistory_Load(object sender, EventArgs e) => await _LoadDataAsync();

        private void btnClose_Click(object sender, EventArgs e) => this.Close();
    }
}
