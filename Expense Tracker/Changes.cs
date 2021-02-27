using System.Linq;

namespace Expense_Tracker
{
    class Changes
    {
        public static void AddExpense(string name, string category, decimal parsedCost, string date)
        {
            // Add expense and substract the expense from users balance
            Database.Users.First(item => item.Username == Database.Session.Username).Expenses.Add(new Model.Expense(name, category, parsedCost, date));
            Database.Users.First(item => item.Username == Database.Session.Username).Money = Database.Users.First(item => item.Username == Database.Session.Username).Money - parsedCost;
        }
        public static void AddIncome(string name, string category, decimal parsedCost, string date)
        {
            // Add income and add the income to users balance
            Database.Users.First(item => item.Username == Database.Session.Username).Incomes.Add(new Model.Income(name, category, parsedCost, date));
            Database.Users.First(item => item.Username == Database.Session.Username).Money = Database.Users.First(item => item.Username == Database.Session.Username).Money + parsedCost;
        }
        public static void RemoveExpense(decimal value)
        {
            // Remove expense and add the expense value to users balance
            Database.Users.First(item => item.Username == Database.Session.Username).Money = Database.Users.First(item => item.Username == Database.Session.Username).Money + value;
        }
        public static void RemoveIncome(decimal value)
        {
            // Remove income and substract the income from users balance
            Database.Users.First(item => item.Username == Database.Session.Username).Money = Database.Users.First(item => item.Username == Database.Session.Username).Money - value;
        }

        public static void ChangeBalance(decimal value)
        {
            // Change users balance to the value specified
            Database.Users.First(item => item.Username == Database.Session.Username).Money = value;
        }
    }
}
