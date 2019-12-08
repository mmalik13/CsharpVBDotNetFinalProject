using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Bank.Models;

namespace Bank {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        BankRepository repo = new BankRepository();
        User user;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.ShowDialog();

            try
            {
                //Get user based on ID provided - [if login successful]
                user = repo.GetUserInfo(int.Parse(login.txtBoxID.Text));
                txtID.Text = user.ID.ToString();
                txtName.Text = user.Name;
                //txtBalance.Text = user.UserAccount.Balance.ToString();
                txtAmount.IsEnabled = true;
                btnDeposit.IsEnabled = true;
                btnWithdraw.IsEnabled = true;
                btnLogin.Visibility = Visibility.Hidden;
                btnLogout.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            

        }

        private void BtnDeposit_Click(object sender, RoutedEventArgs e)
        {
            double tempAmount;

            if(string.IsNullOrEmpty(txtAmount.Text) || !(double.TryParse(txtAmount.Text, out tempAmount)) || !isGreaterThanZero(double.Parse(txtAmount.Text)))
            {
                MessageBox.Show("Amount cannot be empty, \nIt must be a number and greater than zero!");
            }
            else
            {
                user.UserAccount.Balance += tempAmount;
                //txtBalance.Text = repo.DepositAmount(user, tempAmount).ToString("C");
                repo.UpdateUser(user);
                txtBalance.Text = user.UserAccount.Balance.ToString("C");
                MessageBox.Show(tempAmount.ToString("C") + " deposited successfully");

            }
        }

        //Checks if amount can be parsed [Returns bool]
        private bool isParsableDouble(string amount)
        {
            double tempAmount;
            return double.TryParse(amount, out tempAmount);
        }

        private bool isGreaterThanZero(double amount)
        {
            return amount > 0;
        }

        private void BtnWithdraw_Click(object sender, RoutedEventArgs e)
        {
            double withdrawAmount;
            double currentBalance = user.UserAccount.Balance;
            if(!(double.TryParse(txtAmount.Text, out withdrawAmount)) || isGreaterThanZero(double.Parse(txtAmount.Text)) || string.IsNullOrEmpty(txtAmount.Text)){
                MessageBox.Show("Please enter a number to withdraw");
            }
            else
            {
                if(withdrawAmount > currentBalance)
                {
                    MessageBox.Show("Error: Withdraw amount cannot be greather than " + currentBalance);
                }
                else
                {
                    user.UserAccount.Balance = currentBalance - withdrawAmount;
                    repo.UpdateUser(user);
                    txtBalance.Text = user.UserAccount.Balance.ToString();
                    txtAmount.Clear();
                }
            }
        }
    }
}
