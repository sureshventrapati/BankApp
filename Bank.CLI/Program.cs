using System;
using BankApp.Service;

namespace BankApp.CLI
{

    class Program
    {
        static void Main(string[] args)
        {
            bool exit = false;

            BankService bankService = new BankService();
            bankService.AddBank("Money");

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Choose an option...");
                Console.WriteLine("1) Create Account");
                Console.WriteLine("2) Deposit Amount");
                Console.WriteLine("3) Login");
                Console.WriteLine("4) EXIT");
                Console.Write("\n\nEnter your choice: ");

                MainChoice mainChoice = (MainChoice)Enum.Parse(typeof(MainChoice),Console.ReadLine());
                switch (mainChoice)
                {
                    case MainChoice.CreateAccount:
                        Console.Clear();
                        Console.Write("Enter your name: ");
                        string NAME_CREATEACCOUNT = Console.ReadLine();
                        Console.Write("Enter a Password: ");
                        string PASS_CREATEACCOUNT = Console.ReadLine();
                        int ID_CREATEACCOUNT = bankService.CreateAccount(NAME_CREATEACCOUNT,PASS_CREATEACCOUNT);
                        Console.Clear();
                        Console.WriteLine($"Bank account created with account ID: {ID_CREATEACCOUNT}");
                        Console.ReadLine();
                        break;
                    case MainChoice.Deposit:
                        Console.Clear();
                        Console.Write("Enter the AccountID to deposit: ");
                        int ID_DEPOSIT = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter the amount to be deposited: ");
                        int amount = Convert.ToInt32(Console.ReadLine());
                        string NAME_DEPOSIT = bankService.DepositAmount(ID_DEPOSIT,amount);
                        Console.Clear();
                        Console.WriteLine($"{amount}₹ have been deposited into {NAME_DEPOSIT} Account");
                        Console.ReadLine();
                        break;
                    case MainChoice.Login:
                        Console.Clear();
                        Console.Write("Enter your ID: ");
                        int ID_LOGIN = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter your Password: ");
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
                                Console.WriteLine("\n\n1) Transfer Money");
                                Console.WriteLine("2) Logout");

                                Console.Write("Choose an option: ");

                                LoginChoice loginChoice = (LoginChoice)Enum.Parse(typeof(LoginChoice), Console.ReadLine());

                                switch (loginChoice)
                                {
                                    case LoginChoice.TransferMoney:
                                        Console.Write("Enter the ID of the account to whome u want to transfer ammount: ");
                                        int ID_TO = Convert.ToInt32(Console.ReadLine());
                                        Console.Write("Enter the amount to be transfered: ");
                                        int AMOUNT_TRANSFER = Convert.ToInt32(Console.ReadLine());
                                        Console.Clear();
                                        if(bankService.TransferAmount(ID_LOGIN, ID_TO, AMOUNT_TRANSFER))
                                        {
                                            Console.WriteLine("Transaction sucessfully completed !!!");
                                            Console.ReadLine();

                                        }
                                        else
                                        {
                                            Console.WriteLine("Unable to complete transaction as there is insufficient balance.");
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
                            Console.WriteLine("Wrong ID or Password...");
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
