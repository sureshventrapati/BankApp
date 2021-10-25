using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.CLI
{

        public enum MainMenu //Menus
        {
            Deposit = 1,
            Login,
            EXIT,
        }

        public enum CustomerLoginMenu //Menus
        {
            TransferMoney = 1,
            Withdraw,
            ShowTransactions,
            Logout,
        }

        public enum StaffLoginMenu
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

        public enum UpdateCustomerAccountMenu
        {
            UpdateName = 1,
            UpdatePassword,
            Back,
        }
    }
