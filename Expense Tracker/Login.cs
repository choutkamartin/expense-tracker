using System;
using System.Windows.Forms;

namespace Expense_Tracker
{
    public partial class Login : Form
    {

        public Login()
        {
            InitializeComponent();
            Database.Init();
            Database.Deserialize();
        }

        public void LoginButtonClick(object sender, EventArgs e)
        {
            string username = usernameField.Text;
            string password = passwordField.Text;
            bool areCredentialsCorrect = Accounts.LoginSequence(username, password); // Check if credentials entered are ok
            if (areCredentialsCorrect == true)
            {
                Overview overview = new Overview(this);
                overview.Show();
                Hide();
            }
            else
            {
                string message = "No matching credentials in our database.";
                string title = "Error";
                MessageBox.Show(message, title);
            }
        }

        private void SignupButtonClick(object sender, EventArgs e)
        {
            var signup = new Signup();
            signup.Show();
        }

    }
}
