using System;
using System.Threading.Tasks;
using System.Data;
using Hospital_Data;

namespace Hospital_Business
{
    public class clsUser
    {
        public int UserID { get; set; } 
        public string Username { get; set; }
        public string Password { get; set; }
        public byte Role { get; set; }
        public enum enRole { Admin = 1, Receptionist = 2, Doctor = 3 }
        private enRole _Role;

        public DateTime LastLoginDate { get; set; }

        public enum enPermissions { eAll = -1, eAddEditDoctors = 1, eManagePatients = 2,
            eManageAppointments = 4, eManagePayments = 8, eShowMedicalRecords = 16,
            eAddEditMedicalRecords = 32, eManageUsers = 64 }

        public enPermissions Permissions { get; set; }

        public enum enMode { AddNew = 1, Update = 2 }
        private enMode _Mode;

        public string RoleInText()
        {
            switch(this._Role)
            {
                case enRole.Admin:
                    return "Admin";
                 case enRole.Receptionist:
                    return "Receptionist";
                case enRole.Doctor:
                    return "Doctor";
            }
            return string.Empty;
        }
        
        private clsUser(int userID, string username, string password, byte role,
            DateTime lastLoginDate, enPermissions permissions)
        {
            UserID = userID;
            Username = username;
            Password = password;
            Role = role;
            _Role = (enRole)role;
            LastLoginDate = lastLoginDate;
            Permissions = permissions;
            _Mode = enMode.Update;
        }

        public clsUser()
        {
            this.UserID = -1;
            this.Username = null;
            this.Password = null;
            this.Role = 1;
            this._Role = enRole.Admin;
            this.LastLoginDate = DateTime.MinValue;
            this.Permissions = enPermissions.eAll;
            this._Mode = enMode.AddNew;
        }

        public static Task<DataTable>GetAllUsersAysnc() => clsUserData.GetAllUsersAsync();

        public static int GetUsersCount() => clsUserData.GetUsersCount();

        private bool _AddNewUser()
        {
            this.UserID = clsUserData.AddNewUser(this.Username, clsUtil.ComputeHash(this.Password),
                this.Role, Convert.ToInt32(this.Permissions));
            return this.UserID != -1;
        }

        private bool _UpdateUser() => clsUserData.UpdateUser(this.UserID, this.Username,
                this.Role, Convert.ToInt32(this.Permissions));

        public bool Save()
        {
            switch(_Mode)
            {
                case enMode.AddNew:
                    if(this._AddNewUser())
                    {
                        this._Mode = enMode.Update;
                        return true;
                    }
                    break;
                case enMode.Update:
                    return _UpdateUser();
            }
            return false;
        }

        public bool ChangePassword(string NewPassword) => clsUserData.ChangePassword(this.UserID, clsUtil.ComputeHash(NewPassword));

        public static clsUser Find(int UserID)
        {
            string Username = string.Empty;
            string Password = string.Empty;
            byte Role = 0;
            DateTime LastLoginDate = DateTime.MinValue;
            enPermissions permissions = enPermissions.eAll;
            int Permissions = (int)permissions;

            if(clsUserData.Find(UserID, ref Username, ref Password, ref Role,
                ref LastLoginDate, ref Permissions))
                return new clsUser(UserID, Username, Password, Role,
                    LastLoginDate, (enPermissions)Permissions);
            return null;
        }

        public static clsUser Find(string Username, string Password)
        {
            int UserID = -1;
            byte Role = 0;
            DateTime LastLoginDate = DateTime.MinValue;
            enPermissions permissions = enPermissions.eAll;
            int Permissions = (int)permissions;

            if (clsUserData.Find(Username, Password, ref UserID, ref Role,
                ref LastLoginDate, ref Permissions))
                return new clsUser(UserID, Username, Password, Role,
                    LastLoginDate, (enPermissions)Permissions);
            return null;
        }

        public static bool IsUsernameUsed(string Username) => clsUserData.IsUsernameUsed(Username);

        public static bool DeleteUser(int UserID) => clsUserData.DeleteUser(UserID);

        public bool AddAsCurrentUser() => clsUserData.AddAsCurrentUser(UserID) == 1; // 1 for success

        public static bool DoesUserHavePersmissions(clsUser User, enPermissions PermissionToCheck) =>
            ((int)User.Permissions & (int)PermissionToCheck) != 0;

        public bool UpdateUserLastLoginDate() => clsUserData.UpdateUserLastLoginDate(this.UserID);
    }
}