using System.Windows.Forms;

namespace Expense_Tracker
{
    public partial class AddIncome : Form
    {

        readonly Overview ownerForm;

        public AddIncome(Overview ownerForm)
        {
            InitializeComponent();
            this.ownerForm = ownerForm;
        }

        private void AddIncomeButtonClick(object sender, System.EventArgs e)
        {
            string name = nameField.Text;
            string category = categoryListBox.GetItemText(categoryListBox.SelectedItem);
            decimal parsedCost = costNumericUpDown.Value;
            string date = dateOfIncome.Value.ToShortDateString();
            Changes.AddIncome(name, category, parsedCost, date); // Add income into the database
            ownerForm.UserBalanceLoad();
            ownerForm.GridLoadData();
            ownerForm.ColumnChartLoad();
            ownerForm.PieChartLoad();
        }

        private void CloseButtonClick(object sender, System.EventArgs e)
        {
            Close();
        }
    }
}
