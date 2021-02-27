using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Expense_Tracker
{
    class Charts
    {
        public class Expense
        {
            public static List<ColumnChartData> ColumnChartPrepareData()
            {
                // Create new BindingList from users expenses
                var expenses = new BindingList<Model.Expense>(Database.Users.First(item => item.Username == Database.Session.Username).Expenses);

                // Return a list of expenses grouped by date
                return expenses.GroupBy(x => x.Date)
                      .Select(g => new ColumnChartData { Date = g.Key, Amount = g.Sum(x => x.Amount) }).ToList();
            }

            public static List<PieChartData> PieChartPrepareData()
            {
                // Create new BindingList from users expenses
                var expenses = new BindingList<Model.Expense>(Database.Users.First(item => item.Username == Database.Session.Username).Expenses);

                // Return a list of expenses grouped by category
                return expenses
                .GroupBy(l => l.Category)
                    .Select(cl => new PieChartData
                    {
                        Category = cl.First().Category,
                        Amount = cl.Count(),
                    }).ToList();
            }
        }

        public class Income
        {
            public static List<ColumnChartData> ColumnChartPrepareData()
            {
                // Create new BindingList from users incomes
                var income = new BindingList<Model.Income>(Database.Users.First(item => item.Username == Database.Session.Username).Incomes);

                // Return incomes grouped by date, select only dates and summed amount
                return income.GroupBy(x => x.Date)
                      .Select(g => new ColumnChartData { Date = g.Key, Amount = g.Sum(x => x.Amount) }).ToList();
            }

            public static List<PieChartData> PieChartPrepareData()
            {
                // Create new BindingList from users incomes
                var income = new BindingList<Model.Income>(Database.Users.First(item => item.Username == Database.Session.Username).Incomes);
                // Return incomes grouped by category, select only categories and the count of the incomes in the categories
                return income
                .GroupBy(l => l.Category)
                    .Select(cl => new PieChartData
                    {
                        Category = cl.First().Category,
                        Amount = cl.Count(),
                    }).ToList();
            }
        }
    }

    public class ColumnChartData
    {
        public string Date { get; set; }
        public decimal Amount { get; set; }
    }

    public class PieChartData
    {
        public string Category { get; set; }
        public decimal Amount { get; set; }
    }
}
