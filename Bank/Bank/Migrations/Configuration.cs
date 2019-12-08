namespace Bank.Migrations
{
    using Bank.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Bank.Models.BankDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Bank.Models.BankDbContext context)
        {
            //Dropping Data if any
            context.Users.RemoveRange(context.Users);
            context.Accounts.RemoveRange(context.Accounts);

            List<User> users = new List<User>();

            //Admin user
            users.Add(new User()
            {
                Name = "admin",
                Password = "admin",
                UserAccount = new Account()
                {
                    AccountNumber = 0
                }
            });

            //Test User
            users.Add(new User()
            {
                Name = "Test User",
                Password = "123",
                UserAccount = new Account()
                {
                    AccountNumber = User.GenerateAccountNumber(),
                    Balance = 0
                    
                }
            });
            context.Users.AddRange(users);
            base.Seed(context);
        }
    }
}
