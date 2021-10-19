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
        private List<Account> accounts = new List<Account>();
        private int accountNumberCounter=0;


        public bool AddBank(string Name)
        {
            Bank bank = new Bank(Name);
            this.banks.Add(bank);
            return true;
        }

        public int CreateAccount(string Name, string pass)
        {
            this.accountNumberCounter += 1;
            Account acc = new Account(this.accountNumberCounter,Name);
            acc.Passowrd = pass;
            accounts.Add(acc);
            return this.accountNumberCounter;
        }

        public string DepositAmount(int ID,int Amount)
        {
            Account acc = accounts[ID-1];
            acc.balance = acc.balance + Amount;
            Transaction tr = new Transaction(ID, Amount, "Deposit", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            acc.setTransaction(tr);
            return acc.Name;
        }

        public string WithdrawAmount(int ID, int Amount)
        {
            Account acc = accounts[ID - 1];
            int bal = acc.balance;
            if (bal < Amount)
            {
                return "Failed";
            }

            acc.balance = bal - Amount;
            Transaction tr = new Transaction(ID, Amount, "Withdraw", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            acc.setTransaction(tr);
            return "";
        }

        public bool Authenticate(int ID,string pass)
        {
            Account acc = accounts[ID-1];
            if (acc.Passowrd == pass)
            {
                return true;
            }
            return false;
        }

        public string getName(int ID)
        {
            return accounts[ID-1].Name;
        }

        public int getBalance(int ID)
        {
            return accounts[ID-1].balance;
        }

        public bool TransferAmount(int ID_FROM, int ID_TO, int amount)
        {
            Account acc_from = accounts[ID_FROM-1];
            Account acc_to = accounts[ID_TO-1];

            if (acc_from.balance - amount < 0)
            {
                return false;
            }

            Transaction tr = new Transaction(acc_from.ID,acc_to.ID,amount,"Transfer",DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            acc_from.setTransaction(tr);
            Transaction trr = new Transaction(acc_from.ID, acc_to.ID, amount, "Transfer", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            acc_to.setTransaction(trr);

            acc_from.balance -= amount;
            acc_to.balance += amount;

            return true;
        }

        public string GetTransactions(int ID)
        {
            string finaltransactions="";
            Account acc = accounts[ID - 1];
            List<Transaction> transactions = acc.getTransactions();
            foreach(Transaction transaction in transactions)
            {
                finaltransactions += transaction.rID+" "+transaction.desc+" "+transaction.amount+" "+transaction.time+"\n";
            }
            return finaltransactions;
        }

    }
}