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
using System.Windows.Shapes;
using Bank.Models;

namespace Bank {
    /// <summary>
    /// Interaction logic for CreateUserForm.xaml
    /// </summary>
    public partial class CreateUserForm : Window {
        BankRepository repo = new BankRepository();
        User newUser;

        public CreateUserForm()
        {
            InitializeComponent();
        }

        private void BtnAddNewUser_Click(object sender, RoutedEventArgs e)
        {
            if(!(string.IsNullOrEmpty(txtUserName.Text)) && !(string.IsNullOrEmpty(pwdUserPassword.Password)))
            {
                newUser = new User();
                newUser.Name = txtUserName.Text;
                newUser.Password = pwdUserPassword.Password;
                newUser.UserAccount = new Account();
                repo.AddNewUser(newUser);
                MessageBox.Show("User " + newUser.Name + " added successfully");
                this.Close();
            }
            else
            {
                MessageBox.Show("Please enter all fields");
            }
        }
    }
}
