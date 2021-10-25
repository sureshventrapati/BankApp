using System;
using System.Collections.Generic;
using BankApp.Service;
using ConsoleTables;

namespace BankApp.CLI
{

    class Program
    {
        // Console.Read GetNumber Consose.Write

        public static string GetString() //Console.ReadLine
        {
            return Console.ReadLine();
        }

        public static void print(string s) //Console.Write
        {
            Console.Write(s);
        }

        public static void println(string s) //Console.WriteLine
        {
            Console.WriteLine(s);
        }


        public static int GetNumber()
        {
            int Number;
            try
            {
                Number = Convert.ToInt32(Console.Read());
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
            bankService.init();

            while (!exit)
            {
                Console.Clear();
                print(StandardMessage.WelcomeMenu);

                MainMenu MainMenu = (MainMenu)Enum.Parse(typeof(MainMenu),GetString());
                switch (MainMenu)
                {
                    case MainMenu.Deposit:
                        Console.Clear();
                        print(StandardMessage.AskID);
                        string ID_DEPOSIT = GetString();
                        print(StandardMessage.AskDepositAmount);
                        
                        try
                        {
                            int amount = GetNumber();
                            string NAME_DEPOSIT = bankService.DepositAmount(ID_DEPOSIT, amount);
                            Console.Clear();
                            println($"{amount}₹ have been deposited into {NAME_DEPOSIT} Account");
                            GetString();
                            break;
                        }
                        catch
                        {
                            print(StandardMessage.InvalidAccountID);
                            break;
                        }
                    case MainMenu.Login:
                        Console.Clear();
                        string ID_LOGIN, PASS_LOGIN;
                        try
                        {                            
                            println("1) Bankstaff Login\n2) Customer Login");
                            int option = GetNumber();

                            Console.Clear();
                            print(StandardMessage.AskID);
                            ID_LOGIN = GetString();
                            print(StandardMessage.AskPassword);
                            PASS_LOGIN = GetString();
                            Console.Clear();

                            if (option == 1)
                            {
                                if(bankService.AuthenticateStaff(ID_LOGIN, PASS_LOGIN))
                                {
                                    bool e = false;
                                    while (!e)
                                    {
                                        Console.Clear();
                                        print(StandardMessage.StaffLoginMenu);
                                        StaffLoginMenu StaffLoginMenu = (StaffLoginMenu)Enum.Parse(typeof(StaffLoginMenu), GetString());
                                        switch (StaffLoginMenu)
                                        {
                                            case  StaffLoginMenu.CreateAccount:
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
                                                    case  UpdateCustomerAccountMenu.UpdateName:
                                                        Console.Clear();
                                                        print(StandardMessage.AskID);
                                                        string CustomerAccountID =  GetString();
                                                        print(StandardMessage.AskName);
                                                        string NewName = GetString();
                                                        NewName = bankService.UpdateCustomerName(CustomerAccountID,NewName);
                                                        println($"Name has been updated to {NewName}");
                                                        GetString();
                                                        break;
                                                    case UpdateCustomerAccountMenu.UpdatePassword:
                                                        Console.Clear();
                                                        print(StandardMessage.AskID);
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
                                                case  StaffLoginMenu.DeleteAccount:
                                                    Console.Clear();
                                                    print(StandardMessage.AskID);
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
                                                break;
                                            case StaffLoginMenu.UpdatesIMPS:
                                                break;
                                            case StaffLoginMenu.UpdateoRTGS:
                                                break;
                                            case StaffLoginMenu.UpdateoIMPS:
                                                break;
                                            case StaffLoginMenu.ViewAccountTransaction:
                                                Console.Clear();
                                                print(StandardMessage.AskID);
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
                                        int balance = bankService.getBalance(ID_LOGIN);
                                        println($"Welcome {NAME_LOGIN}");
                                        println($"Your account balance is {balance}₹");

                                        print(StandardMessage.LoginMenu);

                                         CustomerLoginMenu loginChoice = (CustomerLoginMenu)Enum.Parse(typeof(CustomerLoginMenu), GetString());

                                        switch (loginChoice)
                                        {
                                            case  CustomerLoginMenu.TransferMoney:
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
                                                try
                                                {
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
                                                }
                                                catch
                                                {
                                                    Console.Clear();
                                                    println(StandardMessage.WithdrawError);
                                                    GetString();
                                                }
                                                break;
                                            case CustomerLoginMenu.ShowTransactions:
                                                try
                                                {
                                                    Console.Clear();
                                                    ConsoleTable t = bankService.GetTransactions(ID_LOGIN);
                                                    t.Write();
                                                    print("\nPress Enter to exit...");
                                                    GetString();

                                                }
                                                catch (Exception ee)
                                                {
                                                    Console.Clear();
                                                    println(StandardMessage.TransactionFetchingError + ee.ToString());
                                                    GetString();
                                                }
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
                            
                        }
                        catch
                        {
                            Console.Clear();
                            println("Enter a valid ID or Password");
                        }
                        break;
                    case  MainMenu.EXIT:
                        exit = true;
                        break;
                }
            }
        }

        

    }
}