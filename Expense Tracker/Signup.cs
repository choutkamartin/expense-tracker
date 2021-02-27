using System;
using System.Windows.Forms;

namespace Expense_Tracker
{
    public partial class Signup : Form
    {
        public Signup()
        {
            InitializeComponent();
        }

        private void SignupButtonClick(object sender, EventArgs e)
        {
            string firstName = FirstNameField.Text;
            string lastName = LastNameField.Text;
            string username = UsernameField.Text;
            string password = PasswordField.Text;
            string passwordAgain = PasswordAgainField.Text;
            decimal parsedAccountBalance = balanceNumericUpDown.Value;

            // Call a method that creates the user record in database and does validation
            string result = Accounts.NewUser(firstName, lastName, username, password, passwordAgain, parsedAccountBalance);
            if (result == "User created")
            {
                Close();
                string message = "User has been created. You can login now.";
                string title = "Success";
                MessageBox.Show(message, title);
            }
            else if (result == "Username already in use")
            {
                string message = "Username is already in use. Please choose a different username.";
                string title = "Error";
                MessageBox.Show(message, title);
            }
            else
            {
                string message = "Passwords don't match. Please check your password.";
                string title = "Error";
                MessageBox.Show(message, title);
            }
        }

        private void ReturnButtonClick(object sender, EventArgs e)
        {
            Close();
        }
    }
}
