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
}
