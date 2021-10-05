using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Model
{
    public class Bank
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Bank(string bankName)
        {
            this.Name = bankName;
        }
    }

    public class Account
    {
        public int balance { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
        public string Passowrd { get; set; }

        public Account(int ID,string Name)
        {
            this.ID = ID;
            this.Name = Name;
            this.balance = 0;
        }
    }
}
