using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Model
{
    public class Bank
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public Bank(string bankName)
        {
            this.Name = bankName;
            this.ID = bankName.Substring(0, 3) + DateTime.Now.ToString("ddMMyyyy");
        }
    }
}
