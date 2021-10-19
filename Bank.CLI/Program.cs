using System;
using System.Collections.Generic;
using BankApp.Service;
using ConsoleTables;

namespace BankApp.CLI
{

    class Program
    {

        private static string bankName = "MoneyBank";
        static void Main(string[] args)
        {
            bool exit = false;

            BankService bankService = new BankService();
            bankService.AddBank(bankName);

            while (!exit)
            {
                Console.Clear();
                Console.Write(StandardMessage.WelcomeMenu());
                

                MainChoice mainChoice = (MainChoice)Enum.Parse(typeof(MainChoice),Console.ReadLine());
                switch (mainChoice)
                {
                    case MainChoice.CreateAccount:
                        Console.Clear();
                        Console.Write(StandardMessage.AskName());
                        string NAME_CREATEACCOUNT = Console.ReadLine();
                        Console.Write(StandardMessage.AskPassword());
                        string PASS_CREATEACCOUNT = Console.ReadLine();
                        string ID_CREATEACCOUNT = bankService.CreateAccount(NAME_CREATEACCOUNT,PASS_CREATEACCOUNT);
                        Console.Clear();
                        Console.WriteLine($"Bank account created with account ID: {ID_CREATEACCOUNT}");
                        Console.ReadLine();
                        break;
                    case MainChoice.Deposit:
                        Console.Clear();
                        Console.Write(StandardMessage.AskID());
                        string ID_DEPOSIT = Console.ReadLine();
                        Console.Write(StandardMessage.AskDepositAmount());
                        
                        int amount = Convert.ToInt32(Console.ReadLine());
                        try
                        {
                            string NAME_DEPOSIT = bankService.DepositAmount(ID_DEPOSIT, amount);
                            Console.Clear();
                            Console.WriteLine($"{amount}₹ have been deposited into {NAME_DEPOSIT} Account");
                            Console.ReadLine();
                            break;
                        }
                        catch
                        {
                            Console.Write(StandardMessage.InvalidAccountID());
                            break;
                        }
                    case MainChoice.Login:
                        Console.Clear();
                        Console.Write(StandardMessage.AskID());
                        string ID_LOGIN = Console.ReadLine();
                        Console.Write(StandardMessage.AskPassword());
                        string PASS_LOGIN = Console.ReadLine();
                        Console.Clear();
                        if(bankService.Authenticate(ID_LOGIN, PASS_LOGIN))
                        {
                            bool e = false;
                            while (!e)
                            {
                                Console.Clear();
                                string NAME_LOGIN = bankService.getName(ID_LOGIN);
                                int balance = bankService.getBalance(ID_LOGIN);
                                Console.WriteLine($"Welcome {NAME_LOGIN}"); 
                                Console.WriteLine($"Your account balance is {balance}₹");

                                Console.Write(StandardMessage.LoginMenu());

                                LoginChoice loginChoice = (LoginChoice)Enum.Parse(typeof(LoginChoice), Console.ReadLine());

                                switch (loginChoice)
                                {
                                    case LoginChoice.TransferMoney:
                                        Console.Write(StandardMessage.TransferAskID());
                                        string ID_TO = Console.ReadLine();
                                        Console.Write(StandardMessage.AskTransferAmount());
                                        int AMOUNT_TRANSFER = Convert.ToInt32(Console.ReadLine());
                                        Console.Clear();
                                        if(bankService.TransferAmount(ID_LOGIN, ID_TO, AMOUNT_TRANSFER))
                                        {
                                            Console.Write(StandardMessage.TransactionSuccess());
                                            Console.ReadLine();

                                        }
                                        else
                                        {
                                            Console.Write(StandardMessage.TransactionErrorInsufficientBal());
                                            Console.ReadLine();
                                        }
                                        break;
                                    case LoginChoice.Withdraw:
                                        Console.Write(StandardMessage.AskWithdrawAmount());
                                        int a = Convert.ToInt32(Console.ReadLine());
                                        try
                                        {
                                            string check = bankService.WithdrawAmount(ID_LOGIN, a);
                                            if (check == "Failed")
                                            {
                                                Console.Clear();
                                                Console.WriteLine(StandardMessage.InsuffiecientFunds());
                                                Console.ReadLine();
                                            }
                                            else
                                            {
                                                Console.Clear();
                                                Console.WriteLine($"{a} has been withdrawed succesfully");
                                                Console.ReadLine();
                                            }
                                        }
                                        catch
                                        {
                                            Console.Clear();
                                            Console.WriteLine(StandardMessage.WithdrawError());
                                            Console.ReadLine();
                                        }
                                        break;
                                    case LoginChoice.ShowTransactions:
                                        try
                                        {
                                            Console.Clear();
                                            ConsoleTable t = bankService.GetTransactions(ID_LOGIN);
                                            t.Write();
                                            Console.Write("\nPress Enter to exit...");
                                            Console.ReadLine();

                                        }
                                        catch(Exception ee)
                                        {
                                            Console.Clear();
                                            Console.WriteLine(StandardMessage.TransactionFetchingError()+ee.ToString());
                                            Console.ReadLine();
                                        }
                                        break;
                                    case LoginChoice.Logout:
                                        e = true;
                                        break;
                                }
                            }
                        }
                        else
                        {
                            Console.Write(StandardMessage.InvalidCredentials());
                            Console.ReadLine();
                        }
                        break;
                    case MainChoice.EXIT:
                        exit = true;
                        break;
                }
            }
        }

        public enum MainChoice
        {
            CreateAccount=1,
            Deposit,
            Login,
            EXIT,
        }

        public enum LoginChoice
        {
            TransferMoney=1,
            Withdraw,
            ShowTransactions,
            Logout,
        }

    }
}