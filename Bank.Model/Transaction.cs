using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Model
{
    public class Transaction
    {
        public string TransactionID;
        public string sID, rID;
        public double amount;
        public string desc, time;

        public Transaction(string TID,string sID, string rID, double amount, string desc, string time)
        {
            this.TransactionID = TID;
            this.sID = sID;
            this.rID = rID;
            this.amount = amount;
            this.desc = desc;
            this.time = time;
        }
        public Transaction(string TID, string rID, double amount, string desc, string time)
        {
            this.TransactionID = TID;
            this.rID = rID;
            this.amount = amount;
            this.desc = desc;
            this.time = time;
        }
    }
}
