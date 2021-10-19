using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.CLI
{
    class StandardMessage
    {
        public static string WelcomeMenu()
        {
            return "Choose an option...\n1) Create Account\n2) Deposit Amount\n3) Login\n4) EXIT\n\nEnter your choice: ";
        }

        public static string AskName()
        {
            return "Enter your name: ";
        }

        public static string AskPassword()
        {
            return "Enter a Password: ";
        }

        public static string AskID()
        {
            return "Enter the AccountID : ";
        }

        public static string AskDepositAmount()
        {
            return "Enter the amount to be deposited: ";
        }

        public static string AskWithdrawAmount()
        {
            return "Enter the amount to withdraw: ";
        }

        public static string InvalidAccountID()
        {
            return "Invlid Account ID !!!";
        }

        public static string LoginMenu()
        {
            return "\n\n1) Transfer Money\n2) Withdraw Money\n3) Show Transactions\n4) Logout\n\nChoose an option:";
        }

        public static string InvalidCredentials()
        {
            return "Wrong ID or Password...";
        }

        public static string TransferAskID()
        {
            return "Enter the ID of the account to whome u want to transfer ammount: ";
        }

        public static string AskTransferAmount()
        {
            return "Enter the amount to be transfered: ";
        }

        public static string TransactionSuccess()
        {
            return "Transaction sucessfully completed !!!";
        }

        public static string TransactionErrorInsufficientBal()
        {
            return "Unable to complete transaction as there is insufficient balance.";
        }

        public static string InsuffiecientFunds()
        {
            return "Insufficient Funds.....";
        }

        public static string WithdrawError()
        {
            return "An error occured while withdrawing...";
        }

        public static string TransactionFetchingError()
        {
            return "An error occured while fetching Transactions...";
        }
    }
}
