using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.CLI
{
    public class Choices
    {
        public enum MainChoice
        {
            Deposit = 1,
            Login,
            EXIT,
        }

        public enum CustomerLoginChoice
        {
            TransferMoney = 1,
            Withdraw,
            ShowTransactions,
            Logout,
        }

        public enum StaffLoginChoice
        {
            CreateAccount = 1,
            UpdateAccount,
            DeleteAccount,
            AddCurrency,
            UpdatesRTGS,
            UpdatesIMPS,
            UpdateoRTGS,
            UpdateoIMPS,
            ViewAccountTransaction,
            RevertTransaction,
            Logout,
        }
    }
}
