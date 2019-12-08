using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Bank.Models;

namespace Bank {
    public class User {

        private BankRepository repo = new BankRepository();
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(20)]
        public string Password { get; set; }

        
        public Account UserAccount { get; set; }

        public User()
        {
            
        }

        public static int GenerateAccountNumber()
        {
            Random random = new Random();

            return random.Next(10) * 100;
        }
    }
}
