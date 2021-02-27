namespace Expense_Tracker.Model
{
    public abstract class Change
    {
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public string Date { get; set; }
        public string Category { get; set; }
    }

    public class Expense : Change
    {

        public Expense(string name, string category, decimal amount, string dateofpayment)
        {
            Name = name;
            Category = category;
            Amount = amount;
            Date = dateofpayment;
        }
        public Expense() { }

    }

    public class Income : Change
    {

        public Income(string name, string category, decimal amount, string dateofpayment)
        {
            Name = name;
            Category = category;
            Amount = amount;
            Date = dateofpayment;
        }
        public Income() { }
    }
}
