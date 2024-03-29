﻿using System;
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
        AdminWindow adminWindow;
        User user;
        private const int ADMIN_ID = 7;

        public Login()
        {
            InitializeComponent();
            this.SizeToContent = SizeToContent.Height;
        }

        private void BtnLoginWindow_Click(object sender, RoutedEventArgs e)
        {   
            
            //Check if input is empty
            if(!(string.IsNullOrEmpty(txtBoxID.Text) && string.IsNullOrEmpty(pwdPassword.Password)))
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
                    //Check if User exists based on ID and password provided
                    user = repo.VerifyLogin(tempID, pwdPassword.Password);
                    
                    if (user == null)
                    {
                        MessageBox.Show("Error: Username or password mismatch!");
                        txtBoxID.Clear();
                        pwdPassword.Clear();
                    }

                    //If Admin ID provided, Open admin window
                    else if (user.ID == ADMIN_ID)
                    {
                        adminWindow = new AdminWindow();
                        adminWindow.ShowDialog();
                        this.Close();
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
