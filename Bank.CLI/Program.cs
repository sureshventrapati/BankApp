using System;
using System.Collections.Generic;
using BankApp.Service;
using ConsoleTables;

namespace BankApp.CLI
{

    class Program
    {

        public static string GetString() 
        {
            return Console.ReadLine();
        }

        public static void print(string s) 
        {
            Console.Write(s);
        }

        public static void println(string s) 
        {
            Console.WriteLine(s);
        }


        public static int GetNumber()
        {
            int Number;
            try
            {
                Number = Convert.ToInt32(Console.ReadLine());
                return Number;
            }
            catch
            {
                println("Only numbers are accepted");
                return -1;
            }
            
        }

        static void Main(string[] args)
        {
            bool exit = false;

            BankService bankService = new BankService();
            println(bankService.init());
            Console.ReadLine();

            while (!exit)
            {
                Console.Clear();
                print(StandardMessage.WelcomeMenu);
                try {
                    MainMenu MainMenu = (MainMenu)Enum.Parse(typeof(MainMenu), GetString());
                    switch (MainMenu)
                    {
                        case MainMenu.Deposit:
                            Console.Clear();
                            print(StandardMessage.AskAccountID);
                            string ID_DEPOSIT = GetString();
                            print(StandardMessage.AskDepositAmount);

                            int amount = GetNumber();
                            string NAME_DEPOSIT = bankService.DepositAmount(ID_DEPOSIT, amount);
                            Console.Clear();
                            println($"{amount}₹ have been deposited into {NAME_DEPOSIT} Account");
                            GetString();
                            break;


                        case MainMenu.Login:
                            Console.Clear();
                            string ID_LOGIN, PASS_LOGIN;

                            print("1) Bankstaff Login\n2) Customer Login\n\nEnter Your Choice: ");
                            int option = GetNumber();

                            Console.Clear();
                            print(StandardMessage.AskAccountID);
                            ID_LOGIN = GetString();

                            print(StandardMessage.AskPassword);
                            PASS_LOGIN = GetString();
                            Console.Clear();

                            if (option == 1)
                            {
                                if (bankService.AuthenticateStaff(ID_LOGIN, PASS_LOGIN))
                                {
                                    bool e = false;
                                    while (!e)
                                    {
                                        Console.Clear();
                                        print(StandardMessage.StaffLoginMenu);
                                        StaffLoginMenu StaffLoginMenu = (StaffLoginMenu)Enum.Parse(typeof(StaffLoginMenu), GetString());
                                        switch (StaffLoginMenu)
                                        {
                                            case StaffLoginMenu.CreateAccount:
                                                Console.Clear();
                                                print(StandardMessage.AskName);
                                                string NAME_CREATEACCOUNT = GetString();
                                                print(StandardMessage.AskPassword);
                                                string PASS_CREATEACCOUNT = GetString();
                                                string ID_CREATEACCOUNT = bankService.CreateCustomerAccount(NAME_CREATEACCOUNT, PASS_CREATEACCOUNT);
                                                Console.Clear();
                                                println($"Bank account created with:\nAccount ID: {ID_CREATEACCOUNT}\nPassword:{PASS_CREATEACCOUNT}");
                                                GetString();
                                                break;

                                            case StaffLoginMenu.UpdateAccount:
                                                Console.Clear();
                                                print(StandardMessage.UpdateCustomerAccount);
                                                UpdateCustomerAccountMenu updateCustomerAccountLoginChoice = (UpdateCustomerAccountMenu)Enum.Parse(typeof(UpdateCustomerAccountMenu), GetString());
                                                switch (updateCustomerAccountLoginChoice)
                                                {
                                                    case UpdateCustomerAccountMenu.UpdateName:
                                                        Console.Clear();
                                                        print(StandardMessage.AskAccountID);
                                                        string CustomerAccountID = GetString();
                                                        print(StandardMessage.AskName);
                                                        string NewName = GetString();
                                                        NewName = bankService.UpdateCustomerName(CustomerAccountID, NewName);
                                                        println($"Name has been updated to {NewName}");
                                                        GetString();
                                                        break;
                                                    case UpdateCustomerAccountMenu.UpdatePassword:
                                                        Console.Clear();
                                                        print(StandardMessage.AskAccountID);
                                                        string CustomerID = GetString();
                                                        print(StandardMessage.AskPassword);
                                                        string NewPassword = GetString();
                                                        NewPassword = bankService.UpdateCustomerPassword(CustomerID, NewPassword);
                                                        print($"Password has been updated to {NewPassword}");
                                                        GetString();
                                                        break;
                                                    case UpdateCustomerAccountMenu.Back:
                                                        Console.Clear();
                                                        break;
                                                }
                                                break;
                                            case StaffLoginMenu.DeleteAccount:
                                                Console.Clear();
                                                print(StandardMessage.AskAccountID);
                                                string CustomerAccID = GetString();
                                                if (bankService.DeleteCustomerAccount(CustomerAccID))
                                                {
                                                    print("Account Found and Deleted !!!");
                                                }
                                                else
                                                {
                                                    println("Account not found in records...");
                                                }
                                                break;
                                            case StaffLoginMenu.AddCurrency:
                                                break;
                                            case StaffLoginMenu.UpdatesRTGS:
                                                print("Enter Bank ID:");
                                                string bankID = GetString();
                                                print("Enter New sRTGS value: ");
                                                int NewsRTGS = GetNumber();
                                                int temp = bankService.UpdatesRTGS(NewsRTGS,bankID);
                                                print($"sRTGS updated to {temp}");
                                                Console.ReadLine();
                                                break;
                                            case StaffLoginMenu.UpdatesIMPS:
                                                print("Enter Bank ID:");
                                                bankID = GetString();
                                                print("Enter New sRTGS value: ");
                                                int NewsIMPS = GetNumber();
                                                temp = bankService.UpdatesRTGS(NewsIMPS, bankID);
                                                print($"sRTGS updated to {temp}");
                                                Console.ReadLine();
                                                break;
                                            case StaffLoginMenu.UpdateoRTGS:
                                                print("Enter Bank ID:");
                                                bankID = GetString();
                                                print("Enter New sRTGS value: ");
                                                int NewoRTGS = GetNumber();
                                                temp = bankService.UpdatesRTGS(NewoRTGS, bankID);
                                                print($"sRTGS updated to {temp}");
                                                Console.ReadLine();
                                                break;
                                            case StaffLoginMenu.UpdateoIMPS:
                                                print("Enter Bank ID:");
                                                bankID = GetString();
                                                print("Enter New sRTGS value: ");
                                                int NewoIMPS = GetNumber();
                                                temp = bankService.UpdatesRTGS(NewoIMPS, bankID);
                                                print($"sRTGS updated to {temp}");
                                                Console.ReadLine();
                                                break;
                                            case StaffLoginMenu.ViewAccountTransaction:
                                                Console.Clear();
                                                print(StandardMessage.AskAccountID);
                                                string ID = GetString();
                                                ConsoleTable ctable = bankService.GetTransactions(ID);
                                                ctable.Write();
                                                print("\nPress Enter to exit...");
                                                GetString();
                                                break;
                                            case StaffLoginMenu.RevertTransaction:
                                                break;
                                            case StaffLoginMenu.Logout:
                                                e = true;
                                                break;
                                            case StaffLoginMenu.ShowBankProfits:
                                                print("Enter BankID: ");
                                                bankID = GetString();
                                                print(bankService.ShowBankProfits(bankID));
                                                Console.ReadLine();
                                                break;
                                        }
                                    }
                                }
                                else
                                {
                                    println(StandardMessage.InvalidCredentials);
                                    GetString();
                                }
                            }
                            else
                            {


                                if (bankService.AuthenticateCustomer(ID_LOGIN, PASS_LOGIN))
                                {
                                    bool e = false;
                                    while (!e)
                                    {
                                        Console.Clear();
                                        string NAME_LOGIN = bankService.getName(ID_LOGIN);
                                        float balance = bankService.getBalance(ID_LOGIN);
                                        println($"Welcome {NAME_LOGIN}");
                                        println($"Your account balance is {balance}₹");

                                        print(StandardMessage.LoginMenu);

                                        CustomerLoginMenu loginChoice = (CustomerLoginMenu)Enum.Parse(typeof(CustomerLoginMenu), GetString());

                                        switch (loginChoice)
                                        {
                                            case CustomerLoginMenu.TransferMoney:
                                                print(StandardMessage.TransferAskID);
                                                string ID_TO = GetString();
                                                print(StandardMessage.AskTransferAmount);
                                                int AMOUNT_TRANSFER = GetNumber();
                                                Console.Clear();
                                                if (bankService.TransferAmount(ID_LOGIN, ID_TO, AMOUNT_TRANSFER))
                                                {
                                                    print(StandardMessage.TransactionSuccess);
                                                    GetString();

                                                }
                                                else
                                                {
                                                    print(StandardMessage.TransactionErrorInsufficientBal);
                                                    GetString();
                                                }
                                                break;
                                            case CustomerLoginMenu.Withdraw:
                                                print(StandardMessage.AskWithdrawAmount);
                                                int a = GetNumber();
                                                //try
                                                //{
                                                string check = bankService.WithdrawAmount(ID_LOGIN, a);
                                                if (check == "Failed")
                                                {
                                                    Console.Clear();
                                                    println(StandardMessage.InsuffiecientFunds);
                                                    GetString();
                                                }
                                                else
                                                {
                                                    Console.Clear();
                                                    println($"{a} has been withdrawed succesfully");
                                                    GetString();
                                                }

                                                break;
                                            case CustomerLoginMenu.ShowTransactions:
                                                Console.Clear();
                                                ConsoleTable t = bankService.GetTransactions(ID_LOGIN);
                                                t.Write();
                                                print("\nPress Enter to exit...");
                                                GetString();
                                                break;
                                            case CustomerLoginMenu.Logout:
                                                e = true;
                                                break;
                                        }
                                    }
                                }
                                else
                                {
                                    Console.Clear();
                                    print(StandardMessage.InvalidCredentials);
                                    GetString();
                                }
                            }
                            break;
                        case MainMenu.EXIT:
                            exit = true;
                            break;
                    }
                }
                catch
                {
                    Console.Clear();
                    println("Error Occured");
                    Console.ReadLine();
                }
                
            }
        }

        

    }
}