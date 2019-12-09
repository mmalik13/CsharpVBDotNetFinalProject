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
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window {
        BankRepository repo = new BankRepository();
        CreateUserForm userForm;
        UpdateUserForm updateForm;
        User user;

        public AdminWindow()
        {
            
            InitializeComponent();
            dgUsers.ItemsSource = repo.GetAllUsers();
        }

        private void BtnAddUser_Click(object sender, RoutedEventArgs e)
        {
            userForm = new CreateUserForm();
            userForm.ShowDialog();
            dgUsers.ItemsSource = repo.GetAllUsers();
        }

        private void BtnUpdateUser_Click(object sender, RoutedEventArgs e)
        {
            if(dgUsers.SelectedIndex == -1)
            {
                MessageBox.Show("To update a user, please select a record!");
            }
            else
            {
                user = dgUsers.SelectedItem as User;
                updateForm = new UpdateUserForm(user);
                updateForm.ShowDialog();
                dgUsers.Items.Refresh();
            }
        }

        private void BtnDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Are you sure you want to delete user?\nThis will also delete related Account","Warning - Delete User", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                user = dgUsers.SelectedItem as User;
                repo.DeleteUser(user.ID);
                MessageBox.Show("Delete Successful");
                dgUsers.ItemsSource = repo.GetAllUsers();
            }
        }
    }
}
