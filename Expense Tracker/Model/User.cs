using System.ComponentModel;

namespace Expense_Tracker.Model
{
    public class User
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public decimal Money { get; set; }

        public User(string firstName, string lastName, string username,
           string password, decimal money)
        {
            FirstName = firstName;
            LastName = lastName;
            Username = username;
            Password = password;
            Money = money;
        }

        public User() { }

        public BindingList<Income> Incomes = new BindingList<Income>();
        public BindingList<Expense> Expenses = new BindingList<Expense>();
    }
}
