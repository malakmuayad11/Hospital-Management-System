using Hospital_Business;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Hospital_System
{
    public class clsValidation
    {
        public static bool ValidateEmail(string Email) =>
            Regex.IsMatch(Email, "^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$");

        public static void ValidateRequiredTextBox(object sender, ErrorProvider errorProvider, CancelEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            if (string.IsNullOrEmpty(txt.Text))
            {
                e.Cancel = true;
                txt.Focus();
                errorProvider.SetError(txt, "This field is required");
            }
            else
            {
                e.Cancel = false;
                errorProvider.SetError(txt, string.Empty);
            }
        }

        public static bool DoesCurrentUserHavePermission(clsUser.enPermissions PermissionToCheck)
        {
            if (!clsUser.DoesUserHavePersmissions(clsGlobal.CurrentUser, PermissionToCheck))
            {
                MessageBox.Show("Sorry, you don't have access to this section, please contact your admin",
                    "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
    }
}
