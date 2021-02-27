using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace Expense_Tracker

{

    public partial class Overview : Form
    {
        readonly Login ownerForm;
        public Overview(Login ownerForm)
        {
            InitializeComponent();
            ColumnChartLoad();
            GridLoadData();
            PieChartLoad();
            LoadUserData();
            UserBalanceLoad();
            this.ownerForm = ownerForm;
        }

        public void LoadUserData() // Loads user data into fields
        {
            firstNameTextBox.Text = Database.Users.First(item => item.Username == Database.Session.Username).FirstName;
            lastNameTextBox.Text = Database.Users.First(item => item.Username == Database.Session.Username).LastName;
            usernameTextBox.Text = Database.Users.First(item => item.Username == Database.Session.Username).Username;
            balanceNumericUpDown.Value = Database.Users.First(item => item.Username == Database.Session.Username).Money;
        }

        public void UserBalanceLoad() // Loads user balance
        {
            CurrentAccountBalanceLabel.Text = Database.Users.First(item => item.Username == Database.Session.Username).Money.ToString();
        }

        public void ColumnChartLoad() // Loads column charts
        {
            expenseColumnChart.DataSource = Charts.Expense.ColumnChartPrepareData();
            expenseColumnChart.DataBind();
            incomeColumnChart.DataSource = Charts.Income.ColumnChartPrepareData();
            incomeColumnChart.DataBind();
        }

        public void PieChartLoad() // Loads pie charts
        {
            expensePieChart.DataSource = Charts.Expense.PieChartPrepareData();
            expensePieChart.DataBind();
            incomePieChart.DataSource = Charts.Income.PieChartPrepareData();
            incomePieChart.DataBind();
        }

        public void GridLoadData() // Loads DataGridView
        {
            expensesDataGridView.AutoGenerateColumns = true;
            expensesDataGridView.DataSource = null;
            expensesDataGridView.Rows.Clear();
            expensesDataGridView.DataSource = new BindingList<Model.Expense>(Database.Users.First(item => item.Username == Database.Session.Username).Expenses);
            incomesDataGridView.AutoGenerateColumns = true;
            incomesDataGridView.DataSource = null;
            incomesDataGridView.Rows.Clear();
            incomesDataGridView.DataSource = new BindingList<Model.Income>(Database.Users.First(item => item.Username == Database.Session.Username).Incomes);
        }

        private void AddExpenseButtonClick(object sender, EventArgs e)
        {
            AddExpense addExpense = new AddExpense(this);
            addExpense.Show();
        }

        private void AddIncomeButtonClick(object sender, EventArgs e)
        {
            AddIncome addIncome = new AddIncome(this);
            addIncome.Show();
        }
        private void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            Database.Serialize(Database.Users, "users.xml"); // Save data to .xml before closing
            ownerForm.Show(); // Show login form
        }

        private void LogoutButtonClick(object sender, EventArgs e)
        {
            Close();
        }

        private void CloseButtonClick(object sender, EventArgs e)
        {
            ownerForm.Close(); // Close login form - will close entire programme
            Database.Serialize(Database.Users, "users.xml"); // Save data to .xml before closing
        }

        private void RemoveSelectedExpenseButtonClick(object sender, EventArgs e)
        {
            if (expensesDataGridView.SelectedRows.Count > 0) // Check if user selected a row
            {
                int rowIndex = expensesDataGridView.CurrentCell.RowIndex;
                decimal value = decimal.Parse(expensesDataGridView.SelectedRows[0].Cells[1].Value.ToString());
                Changes.RemoveExpense(value); // Remove record from database
                expensesDataGridView.Rows.RemoveAt(rowIndex); // Remove record from DataGridView
                UserBalanceLoad();
                PieChartLoad();
                ColumnChartLoad();
            }
            else
            {
                string message = "You have to choose a row when trying to delete a record.";
                string title = "Error";
                MessageBox.Show(message, title);
            }
        }

        private void RemoveSelectedIncomeButtonClick(object sender, EventArgs e)
        {
            if (incomesDataGridView.SelectedRows.Count > 0) // Check if user selected a row
            {
                int rowIndex = incomesDataGridView.CurrentCell.RowIndex;
                decimal value = decimal.Parse(incomesDataGridView.SelectedRows[0].Cells[1].Value.ToString());
                Changes.RemoveIncome(value); // Remove record from database
                incomesDataGridView.Rows.RemoveAt(rowIndex); // Remove record from DataGridView
                UserBalanceLoad();
                PieChartLoad();
                ColumnChartLoad();
            }
            else
            {
                string message = "You have to choose a row when trying to delete a record";
                string title = "Error";
                MessageBox.Show(message, title);
            }
        }

        private void SaveChangesButtonClick(object sender, EventArgs e)
        {
            string firstName = firstNameTextBox.Text;
            string lastName = lastNameTextBox.Text;
            Accounts.UpdateAccountDetails(firstName, lastName); // Update user account in the database
            LoadUserData();
        }

        private void ChangePasswordButtonClick(object sender, EventArgs e)
        {
            var changePassword = new ChangePassword();
            changePassword.Show();
        }

        private void DeleteAccountButtonClick(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Do you really want to delete your account? All data will be deleted.", "Are you sure?", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                bool isAccountDeleted = Accounts.DeleteAccount(); // Delete user and user data
                if (isAccountDeleted == true)
                {
                    Close();
                }
            }
            else if (dialogResult == DialogResult.No)
            {
            }
        }

        private void ChangeBalanceButtonClick(object sender, EventArgs e)
        {
            decimal value = balanceNumericUpDown.Value;
            Changes.ChangeBalance(value); // Change user's balance in the database
            LoadUserData();
            UserBalanceLoad();
        }
    }
}
