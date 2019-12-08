using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Models {
    class BankRepository {
        BankDbContext context = new BankDbContext();

        //Check if User exists in DB
        public User VerifyLogin(int ID, string password)
        {
            return (from u in context.Users
                    where u.ID == ID
                    select u).SingleOrDefault();
        }

        //Get user from Db
        public User GetUserInfo(int ID)
        {
            return (from u in context.Users
                    where u.ID == ID
                    select u).SingleOrDefault();
        }

        //public double DepositAmount(User user, double amount)
        //{
            
            
            
            
        //}

        public void UpdateUser(User user)
        {
            User currentUser = GetUserInfo(user.ID);
            //user.UserAccount.Balance = amount;
            currentUser.Name = user.Name;
            currentUser.Password = user.Password;
            currentUser.UserAccount = user.UserAccount;

            context.SaveChanges();
        }
    }
}
