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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window {
        private BankRepository repo = new BankRepository();

        public Login()
        {
            InitializeComponent();
            this.SizeToContent = SizeToContent.Height;
        }

        private void BtnLoginWindow_Click(object sender, RoutedEventArgs e)
        {
            //Check if input is empty
            if(!(string.IsNullOrEmpty(txtBoxID.Text) && string.IsNullOrEmpty(txtBoxPassword.Text)))
            {
                int tempID;

                //Check if ID is an int
                if (!(int.TryParse(txtBoxID.Text, out tempID)))
                {
                    MessageBox.Show("ID must be a number only!");
                    return;
                }
                else
                {
                    User user = repo.VerifyLogin(tempID, txtBoxPassword.Text);
                    
                    //Check if User exists based on ID and password provided
                    if (user == null)
                    {
                        MessageBox.Show("Error: Username or password mismatch!");
                    }
                    else
                    {
                        MessageBox.Show("Welcome, " + user.Name);
                        this.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please enter all fields!");
                return;
            }
        }
    }
}
