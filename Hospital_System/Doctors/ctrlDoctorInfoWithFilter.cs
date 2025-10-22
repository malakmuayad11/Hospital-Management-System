using Hospital_Business;
using System;
using System.Windows.Forms;

namespace Hospital_System.Doctors
{
    public partial class ctrlDoctorInfoWithFilter : UserControl
    {
        public ctrlDoctorInfoWithFilter() => InitializeComponent();

        public int DoctorID { get => ctrlDoctorInfo1.DoctorID; }
        private bool _IsDoctorFound = false;

        public event EventHandler<bool> FindDoctor;

        public void OnFindDoctor(bool isFound) => FindDoctor?.Invoke(this, isFound);

        private void btnAddNewDoctor_Click(object sender, EventArgs e)
        {
            if(!clsValidation.DoesCurrentUserHavePermission(clsUser.enPermissions.eAddEditDoctors))
                return;

            frmAddEditDoctor frm = new frmAddEditDoctor();
            frm.DoctorAdded += Frm_DoctorAdded;
            frm.ShowDialog();
        }

        private void Frm_DoctorAdded(object sender, clsDoctor Doctor)
        {
            ctrlDoctorInfo1.LoadDoctorInfo(Doctor.DoctorID);
            txtDoctorID.Text = Doctor.DoctorID.ToString();
        }

        private void txtDoctorID_KeyPress(object sender, KeyPressEventArgs e) =>
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDoctorID.Text))
            {
                ctrlDoctorInfo1.OnDoctorLoad += CtrlDoctorInfo1_OnDoctorLoad;
                ctrlDoctorInfo1.LoadDoctorInfo(Convert.ToInt32(txtDoctorID.Text));
                OnFindDoctor(_IsDoctorFound);
            }
            else
                MessageBox.Show("Please enter the Doctor ID", "Doctor ID is empty",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void CtrlDoctorInfo1_OnDoctorLoad(object sender, ctrlDoctorInfo.DoctorEventArgs e) =>
            _IsDoctorFound = e.IsFound;
    }
}
