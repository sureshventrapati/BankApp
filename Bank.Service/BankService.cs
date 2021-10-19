using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApp.Model;

namespace BankApp.Service
{
    public class BankService
    {
        private List<Bank> banks = new List<Bank>();
        //private List<Account> accounts = new List<Account>();
        private Dictionary<string, Account> Accounts = new Dictionary<string, Account>();
        //private int accountNumberCounter=0;


        public bool AddBank(string Name)
        {
            Bank bank = new Bank(Name);
            this.banks.Add(bank);
            return true;
        }

        public string CreateAccount(string Name, string pass)
        {
            //this.accountNumberCounter += 1;
            Account acc = new Account(Name);
            acc.Passowrd = pass;
            //accounts.Add(acc);
            string AccountID = acc.AccountID;
            Accounts.Add(AccountID,acc);
            return AccountID;
        }

        public string DepositAmount(string AccountID,int Amount)
        {
            Account acc = Accounts[AccountID];
            //Account acc = accounts[ID-1];
            acc.balance = acc.balance + Amount;
            Transaction tr = new Transaction(AccountID, Amount, "Deposit", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            acc.setTransaction(tr);
            return acc.Name;
        }

        public string WithdrawAmount(string AccountID, int Amount)
        {
            Account acc = Accounts[AccountID];
            //Account acc = accounts[ID - 1];
            int bal = acc.balance;
            if (bal < Amount)
            {
                return "Failed";
            }

            acc.balance = bal - Amount;
            Transaction tr = new Transaction(AccountID, Amount, "Withdraw", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            acc.setTransaction(tr);
            return "";
        }

        public bool Authenticate(string AccountID,string pass)
        {
            Account acc = Accounts[AccountID];
            //Account acc = accounts[ID-1];
            if (acc.Passowrd == pass)
            {
                return true;
            }
            return false;
        }

        public string getName(string AccountID)
        {
            return Accounts[AccountID].Name;
        }

        public int getBalance(string AccountID)
        {
            return Accounts[AccountID].balance;
        }

        public bool TransferAmount(string ID_FROM, string ID_TO, int amount)
        {
            Account acc_from = Accounts[ID_FROM];
            Account acc_to = Accounts[ID_TO];
            //Account acc_from = accounts[ID_FROM-1];
            //Account acc_to = accounts[ID_TO-1];

            if (acc_from.balance - amount < 0)
            {
                return false;
            }

            Transaction tr = new Transaction(acc_from.AccountID,acc_to.AccountID,amount,"Transfer",DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            acc_from.setTransaction(tr);
            Transaction trr = new Transaction(acc_from.AccountID, acc_to.AccountID, amount, "Transfer", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            acc_to.setTransaction(trr);

            acc_from.balance -= amount;
            acc_to.balance += amount;

            return true;
        }

        public string GetTransactions(string AccountID)
        {
            string finaltransactions="";
            Account acc = Accounts[AccountID];
            //Account acc = accounts[ID - 1];
            List<Transaction> transactions = acc.getTransactions();
            foreach(Transaction transaction in transactions)
            {
                finaltransactions += transaction.rID+" "+transaction.desc+" "+transaction.amount+" "+transaction.time+"\n";
            }
            return finaltransactions;
        }

    }
}