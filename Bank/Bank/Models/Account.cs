using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Models {
    public class Account {

        public Account()
        {
            Balance = 0;
        }

        [Key]
        public int AccountNumber { get; set; }
        
        public double Balance { get; set; }
    }
}
