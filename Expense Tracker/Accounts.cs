using System;
using System.Linq;

namespace Expense_Tracker
{
    class Accounts
    {
        public static bool ComparePassword(string currentPassword, string newPassword, string newPasswordRepeat)
        {
            // Check if entered password matches with the password from database
            if (currentPassword == Database.Users.First(item => item.Username == Database.Session.Username).Password)
            {
                // Check if new password and a repeated one match
                if (newPassword == newPasswordRepeat)
                {
                    Database.Users.First(user => user.Username == Database.Session.Username).Password = newPassword;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static string NewUser(string firstName, string lastName, string username, string password, string passwordAgain, decimal parsedAccountBalance)
        {
            // Check if passwords match
            if (password == passwordAgain)
            {
                // Check if user with same username exists
                var userExists = Database.Users.FirstOrDefault(item => item.Username == username);
                if (userExists == null)
                {
                    Database.Users.Add(new Model.User(firstName, lastName, username, password, parsedAccountBalance));
                    return "User created";
                }
                else
                {
                    return "Username already in use";
                }
            }
            else
            {
                return "Passwords don't match";
            }
        }

        public static bool LoginSequence(string username, string password)
        {
            try
            {
                // Get data from datase, if there are no matching users, application will fire up an exception that gets catched 
                var usernameDatabase = Database.Users.First(user => user.Username == username).Username;
                var passwordDatabase = Database.Users.First(user => user.Username == username).Password;
                if (password == passwordDatabase)
                {
                    // Store users username to the session
                    Database.Session.Username = usernameDatabase;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static void UpdateAccountDetails(string firstName, string lastName)
        {
            Database.Users.First(user => user.Username == Database.Session.Username).FirstName = firstName;
            Database.Users.First(user => user.Username == Database.Session.Username).LastName = lastName;
        }

        public static bool DeleteAccount()
        {
            Database.Users.Remove(Database.Users.Where(item => item.Username == Database.Session.Username).First());
            return true;
        }
    }
}
