using System;
using System.Windows.Forms;

namespace Expense_Tracker
{
    public partial class ChangePassword : Form
    {
        public ChangePassword()
        {
            InitializeComponent();
        }

        private void ChangePasswordButtonClick(object sender, EventArgs e)
        {
            var currentPassword = currentPasswordField.Text;
            var newPassword = newPasswordField.Text;
            var newPasswordRepeat = newPasswordRepeatField.Text;
            bool result = Accounts.ComparePassword(currentPassword, newPassword, newPasswordRepeat); // Check if passwords entered match
            if (result == true) {
                string message = "Password was succesfully changed.";
                string title = "Success";
                MessageBox.Show(message, title);
            }
            else
            {
                string message = "Password wasn't changed. Check credentials you entered.";
                string title = "Error";
                MessageBox.Show(message, title);
            }
        }

        private void CloseButtonClick(object sender, EventArgs e)
        {
            Hide();
        }
    }
}
