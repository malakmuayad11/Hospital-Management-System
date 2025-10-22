using System;
using Hospital_Business;
using System.Windows.Forms;

namespace Hospital_System
{
    public partial class ctrlUserInfo : UserControl
    {
        private clsUser _User;
        public ctrlUserInfo() => InitializeComponent();

        public clsUser User
        {
            get => _User;
        }

        public void LoadUserData(int UserID)
        {
            _User = clsUser.Find(UserID);
            if(_User == null )
            {
                MessageBox.Show("An error occurred while loading user data.");
                return;
            }
            lblUserID.Text = UserID.ToString();
            lblUsername.Text = _User.Username;
            lblRole.Text = _User.RoleInText();
        }
    }
}
