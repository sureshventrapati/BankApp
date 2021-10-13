using System;
using BankApp.Service;

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
                Console.WriteLine(StandardMessage.WelcomeMenu());
                

                MainChoice mainChoice = (MainChoice)Enum.Parse(typeof(MainChoice),Console.ReadLine());
                switch (mainChoice)
                {
                    case MainChoice.CreateAccount:
                        Console.Clear();
                        Console.WriteLine(StandardMessage.AskName());
                        string NAME_CREATEACCOUNT = Console.ReadLine();
                        Console.WriteLine(StandardMessage.AskPassword());
                        string PASS_CREATEACCOUNT = Console.ReadLine();
                        int ID_CREATEACCOUNT = bankService.CreateAccount(NAME_CREATEACCOUNT,PASS_CREATEACCOUNT);
                        Console.Clear();
                        Console.WriteLine($"Bank account created with account ID: {ID_CREATEACCOUNT}");
                        Console.ReadLine();
                        break;
                    case MainChoice.Deposit:
                        Console.Clear();
                        Console.WriteLine(StandardMessage.AskID());
                        int ID_DEPOSIT = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine(StandardMessage.AskDepositAmount());
                        
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
                            Console.WriteLine(StandardMessage.InvalidAccountID());
                            break;
                        }
                    case MainChoice.Login:
                        Console.Clear();
                        Console.Write(StandardMessage.AskID());
                        int ID_LOGIN = Convert.ToInt32(Console.ReadLine());
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

                                Console.WriteLine(StandardMessage.LoginMenu());

                                LoginChoice loginChoice = (LoginChoice)Enum.Parse(typeof(LoginChoice), Console.ReadLine());

                                switch (loginChoice)
                                {
                                    case LoginChoice.TransferMoney:
                                        Console.Write(StandardMessage.TransferAskID());
                                        int ID_TO = Convert.ToInt32(Console.ReadLine());
                                        Console.Write(StandardMessage.AskTransferAmount());
                                        int AMOUNT_TRANSFER = Convert.ToInt32(Console.ReadLine());
                                        Console.Clear();
                                        if(bankService.TransferAmount(ID_LOGIN, ID_TO, AMOUNT_TRANSFER))
                                        {
                                            Console.WriteLine(StandardMessage.TransactionSuccess());
                                            Console.ReadLine();

                                        }
                                        else
                                        {
                                            Console.WriteLine(StandardMessage.TransactionErrorInsufficientBal());
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
                            Console.WriteLine(StandardMessage.InvalidCredentials());
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
            Logout,
        }

    }
}
