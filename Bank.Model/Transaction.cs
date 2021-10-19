using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Model
{
    public class Transaction
    {
        public string sID, rID;
        public double amount;
        public string desc, time;

        public Transaction(string sID, string rID, double amount, string desc, string time)
        {
            this.sID = sID;
            this.rID = rID;
            this.amount = amount;
            this.desc = desc;
            this.time = time;
        }
        public Transaction(string rID, double amount, string desc, string time)
        {
            this.rID = rID;
            this.amount = amount;
            this.desc = desc;
            this.time = time;
        }
    }
}
