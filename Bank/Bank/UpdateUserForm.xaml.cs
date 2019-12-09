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
    /// Interaction logic for UpdateUserForm.xaml
    /// </summary>
    public partial class UpdateUserForm : Window {
        BankRepository repo = new BankRepository();
        User user;

        public UpdateUserForm()
        {
            InitializeComponent();
            
        }

        public UpdateUserForm(User _user)
        {
            InitializeComponent();
            user = _user;
            txtID.Text = user.ID.ToString();
            txtAccountNumber.Text = user.UserAccount.AccountNumber.ToString();
        }

        private void BtnUpdateUser_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(txtNewName.Text) || string.IsNullOrEmpty(pwdNewPassword.Password))
            {
                MessageBox.Show("Name and Password fields cannot be empty");
            }
            else
            {
                
                user.Name = txtNewName.Text;
                user.Password = pwdNewPassword.Password;
                user.UserAccount = repo.GetUserAccount(user.ID);
                
                repo.UpdateUser(user);
                MessageBox.Show("Update successful");
                this.Close();
            }
        }
    }
}
