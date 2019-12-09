using System;
using System.Collections.Generic;
using System.IO;
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
        private const int ADMIN_ID = 7;

        //Path to transaction history file
        string transactionHistoryFile = @"C:\Users\Mohammed Malik\Documents\transaction-history.txt";

        public MainWindow()
        {
            InitializeComponent();
        }

        #region Button Events
        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.ShowDialog();

            try
            {
                //Get user based on ID provided - [if login successful]
                
                user = repo.GetUserInfo(int.Parse(login.txtBoxID.Text));
                user.UserAccount = repo.GetUserAccount(user.ID);
                if(user.ID == ADMIN_ID)
                {
                    return;
                }
                else
                {
                    txtID.Text = user.ID.ToString();
                    txtName.Text = user.Name;
                    txtBalance.Text = user.UserAccount.Balance.ToString("C");
                    txtAmount.IsEnabled = true;
                    btnDeposit.IsEnabled = true;
                    btnWithdraw.IsEnabled = true;
                    btnLogin.Visibility = Visibility.Hidden;
                    btnLogout.Visibility = Visibility.Visible;
                }
                
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
                try
                {
                    user.UserAccount.Balance += tempAmount;
                    //txtBalance.Text = repo.DepositAmount(user, tempAmount).ToString("C");
                    repo.UpdateUser(user);
                    txtBalance.Text = user.UserAccount.Balance.ToString("C");
                    txtAmount.Clear();

                    MessageBox.Show(tempAmount.ToString("C") + " deposited successfully");
                    string transaction = DateTime.Now.ToString() + 
                                        " #" + user.UserAccount.AccountNumber + " | D | " + 
                                        (user.UserAccount.Balance - tempAmount).ToString("C") + " | " 
                                        + user.UserAccount.Balance.ToString("C") + "\n";

                    WriteToFile(transaction);
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Error: " + exc.Message); 
                }
                

            }
        }

        private void BtnWithdraw_Click(object sender, RoutedEventArgs e)
        {
            double withdrawAmount;
            double currentBalance = user.UserAccount.Balance;
            if(!(double.TryParse(txtAmount.Text, out withdrawAmount)) || !(isGreaterThanZero(double.Parse(txtAmount.Text))) || (string.IsNullOrEmpty(txtAmount.Text))){
                MessageBox.Show("Please enter a number to withdraw");
            }
            else
            {
                if(withdrawAmount > currentBalance)
                {
                    MessageBox.Show("Error: Withdraw amount cannot be greather than " + currentBalance.ToString("C"));
                }
                else
                {
                    try
                    {
                        user.UserAccount.Balance = currentBalance - withdrawAmount;
                        repo.UpdateUser(user);
                        MessageBox.Show("Withdraw of " + withdrawAmount.ToString("C") + " successful");
                        txtBalance.Text = user.UserAccount.Balance.ToString("C");
                        txtAmount.Clear();

                        string transaction = DateTime.Now.ToString() +
                                        " #" + user.UserAccount.AccountNumber + " | W | " +
                                        (user.UserAccount.Balance + withdrawAmount).ToString("C") + " | "
                                        + user.UserAccount.Balance.ToString("C") + "\n";

                        WriteToFile(transaction);
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                    
                }
            }
        }

        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            txtName.Text = string.Empty;
            txtID.Text = string.Empty;
            txtBalance.Text = string.Empty;
            btnLogout.Visibility = Visibility.Hidden;
            btnLogin.Visibility = Visibility.Visible;
            btnDeposit.IsEnabled = false;
            btnWithdraw.IsEnabled = false;
        }

        #endregion

        #region Helper Methods
        private bool isGreaterThanZero(double amount)
        {
            return amount > 0;
        }

        private void WriteToFile(string content)
        {
            StreamWriter writer;

            try
            {
                using (writer = new StreamWriter(transactionHistoryFile, true))
                {
                    writer.Write(content);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: " + exc.Message);
            }
            
        }
        #endregion
    }
}
