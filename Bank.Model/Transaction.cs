using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Model
{
    public class Transaction
    {
        public int sID, rID;
        public double amount;
        public string desc, time;

        public Transaction(int sID, int rID, double amount, string desc, string time)
        {
            this.sID = sID;
            this.rID = rID;
            this.amount = amount;
            this.desc = desc;
            this.time = time;
        }
        public Transaction(int rID, double amount, string desc, string time)
        {
            this.rID = rID;
            this.amount = amount;
            this.desc = desc;
            this.time = time;
        }
    }
}
