using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApp.Model;
using ConsoleTables;

namespace BankApp.Service
{
    public class BankService
    {
        private Dictionary<string, Bank> banks = new Dictionary<string, Bank>();
        
        private Dictionary<string, Account> customerAccounts = new Dictionary<string, Account>();

        private Dictionary<string, StaffAccount> staffAccounts = new Dictionary<string, StaffAccount>();

        public void init() {
            string bankName = "MoneyBank";
            this.AddBank(bankName); 
            this.CreateStaffAccount("admin", "admin");
        }


        public bool AddBank(string Name, int sRTGS, int sIMPS, int oRTGS, int oIMPS)
        {
            Bank bank = new Bank(Name, sRTGS, sIMPS, oRTGS, oIMPS);
            this.banks.Add(bank.ID,bank);
            return true;
        }

        public bool AddBank(string Name)
        {
            Bank bank = new Bank(Name);
            this.banks.Add(bank.ID, bank);
            return true;
        }

        public string CreateCustomerAccount(string Name, string pass)
        {
            Account acc = new Account(Name);
            acc.Passowrd = pass;
            acc.bankID = "Mon19102021";
            string AccountID = acc.AccountID;
            customerAccounts.Add(AccountID,acc);
            return AccountID;
        }

        public string CreateStaffAccount(string Name, string pass)
        {
            StaffAccount acc = new StaffAccount("admin",Name,pass);
            acc.Passowrd = pass;
            acc.AccountID = "admin";
            staffAccounts.Add("admin",acc);
            return "admin";
        }

        public string DepositAmount(string AccountID,int Amount)
        {
            Account acc = customerAccounts[AccountID];
            acc.balance = acc.balance + Amount;
            string TID = acc.bankID + acc.AccountID + DateTime.Now.ToString("HHmmss");
            Transaction tr = new Transaction(TID, AccountID, Amount, "Deposit", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            acc.setTransaction(tr);
            return acc.Name;
        }

        public string WithdrawAmount(string AccountID, int Amount)
        {
            Account acc = customerAccounts[AccountID];
            int bal = acc.balance;
            if (bal < Amount)
            {
                return "Failed";
            }

            acc.balance = bal - Amount;
            string TID = acc.bankID + acc.AccountID + DateTime.Now.ToString("HHmmss");
            Transaction tr = new Transaction(TID, AccountID, Amount, "Withdraw", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            acc.setTransaction(tr);
            return "";
        }

        public bool AuthenticateCustomer(string AccountID,string pass)
        {
            Account acc = customerAccounts[AccountID];
            if (acc.Passowrd == pass)
            {
                return true;
            }
            return false;
        }

        public bool AuthenticateStaff(string AccountID, string pass)
        {
            StaffAccount acc = staffAccounts[AccountID];
            if (acc.Passowrd == pass)
            {
                return true;
            }
            return false;
        }

        public string UpdateCustomerName(string AccountID,string NewName)
        {
            Account acc = customerAccounts[AccountID];
            acc.Name = NewName;
            return NewName;
        }

        public string UpdateCustomerPassword(string AccountID, string NewPassword)
        {
            Account acc = customerAccounts[AccountID];
            acc.Passowrd = NewPassword;
            return NewPassword;
        }

        public bool DeleteCustomerAccount(string AccountID)
        {
            return customerAccounts.Remove(AccountID);
        }

        public string getName(string AccountID)
        {
            return customerAccounts[AccountID].Name;
        }

        public int getBalance(string AccountID)
        {
            return customerAccounts[AccountID].balance;
        }

        public bool TransferAmount(string ID_FROM, string ID_TO, int amount)
        {
            Account acc_from = customerAccounts[ID_FROM];
            Account acc_to = customerAccounts[ID_TO];

            if (acc_from.balance - amount < 0)
            {
                return false;
            }

            string TID_FROM = acc_from.bankID + acc_from.AccountID + DateTime.Now.ToString("HHmmss");
            Transaction tr = new Transaction(TID_FROM,acc_from.AccountID,acc_to.AccountID,amount,"Transfer",DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            acc_from.setTransaction(tr);
            string TID_TO = acc_to.bankID + acc_to.AccountID + DateTime.Now.ToString("HHmmss");
            Transaction trr = new Transaction(TID_TO,acc_from.AccountID, acc_to.AccountID, amount, "Transfer", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            acc_to.setTransaction(trr);

            acc_from.balance -= amount;
            acc_to.balance += amount;

            return true;
        }

        public ConsoleTable GetTransactions(string AccountID)
        {
            Account acc = customerAccounts[AccountID];
            List<Transaction> transactions = acc.getTransactions();
            ConsoleTable table = new ConsoleTable(new ConsoleTableOptions { Columns = new[] { "TransactionID", "SendersAccountID", "RecieversAccountID", "Type", "Amount", "Time" }, EnableCount = false });
            foreach(Transaction transaction in transactions)
            {
                table.AddRow(transaction.TransactionID, transaction.sID, transaction.rID, transaction.desc, transaction.amount, transaction.time);
            }
            return table;
        }


    }
}