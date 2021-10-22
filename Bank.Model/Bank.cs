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

        public int sRTGSCharge { get; set; }

        public int sIMPSCharge { get; set; }

        public int oRTGSCharge { get; set; }

        public int oIMPSCharge { get; set; }


        public Bank(string bankName,int sRTGS, int sIMPS, int oRTGS, int oIMPS)
        {
            this.Name = bankName;
            this.ID = bankName.Substring(0, 3) + DateTime.Now.ToString("ddMMyyyy");
            this.sRTGSCharge = sRTGS;
            this.sIMPSCharge = sIMPS;
            this.oRTGSCharge = oRTGS;
            this.oIMPSCharge = oIMPS;

        }

        public Bank(string bankName)
        {
            this.Name = bankName;
            this.ID = bankName.Substring(0, 3) + DateTime.Now.ToString("ddMMyyyy");
            this.sRTGSCharge = 0;
            this.sIMPSCharge = 5;
            this.oRTGSCharge = 2;
            this.oIMPSCharge = 6;

        }
    }
}
