using System;
using System.Windows.Forms;

namespace Expense_Tracker
{
    public partial class AddExpense : Form
    {
        readonly Overview ownerForm;

        public AddExpense(Overview ownerForm)
        {
            InitializeComponent();
            this.ownerForm = ownerForm;
        }

        private void AddExpenseButtonClick(object sender, EventArgs e)
        {
            string name = nameField.Text;
            string category = categoryListBox.GetItemText(categoryListBox.SelectedItem);
            decimal parsedCost = costNumericUpDown.Value;
            var date = dateOfPayment.Value.ToShortDateString();
            Changes.AddExpense(name, category, parsedCost, date); // Add expense into the database
            ownerForm.UserBalanceLoad();
            ownerForm.GridLoadData();
            ownerForm.ColumnChartLoad();
            ownerForm.PieChartLoad();
        }

        private void CloseButtonClick(object sender, EventArgs e)
        {
            Close();
        }
    }
}
