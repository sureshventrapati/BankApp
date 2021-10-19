using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Model
{
    public class Account
    {
        public List<Transaction> transactions = new List<Transaction>();

        public string bankID { get; set; }

        public string AccountID { get; set; }
        public int balance { get; set; }
        //public int ID { get; set; }
        public string Name { get; set; }
        public string Passowrd { get; set; }

        public Account(string Name)
        {
            this.AccountID = Name.Substring(0, 3) + DateTime.Now.ToString("ddMMyyyyHHmmss");
            //this.ID = ID;
            this.Name = Name;
            this.balance = 0;
        }

        public List<Transaction> getTransactions()
        {
            return transactions;
        }

        public bool setTransaction(Transaction t)
        {
            transactions.Add(t);
            return true;
        }
    }
}
