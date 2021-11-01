﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Model
{
    public class Transaction
    {
        public static int Total { get; set; }
        public int SNO { get; set; }
        public string TransactionID { get; set; }
        public string sID { get; set; }
        public string rID { get; set; }
        public float amount { get; set; }
        public string desc { get; set; }
        public string time { get; set; }

        public Transaction(string TID,string sID, string rID, float amount, string desc, string time)
        {
            this.TransactionID = TID;
            this.sID = sID;
            this.rID = rID;
            this.amount = amount;
            this.desc = desc;
            this.time = time;
            Total += 1;
        }
        public Transaction(string TID, string rID, float amount, string desc, string time)
        {
            this.TransactionID = TID;
            this.rID = rID;
            this.amount = amount;
            this.desc = desc;
            this.time = time;
            Total += 1;
        }
    }
}
