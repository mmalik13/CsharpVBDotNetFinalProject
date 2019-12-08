﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Models {
    class BankDbContext : DbContext {
        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }

    }
}
