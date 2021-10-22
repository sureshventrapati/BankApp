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

            Choices choice = new Choices();

            BankService bankService = new BankService();
            bankService.AddBank(bankName);
            bankService.CreateStaffAccount("admin", "admin");

            while (!exit)
            {
                Console.Clear();
                Console.Write(StandardMessage.WelcomeMenu());

                Choices.MainChoice mainChoice = (Choices.MainChoice)Enum.Parse(typeof(Choices.MainChoice),Console.ReadLine());
                switch (mainChoice)
                {
                    case Choices.MainChoice.Deposit:
                        Console.Clear();
                        Console.Write(StandardMessage.AskID());
                        string ID_DEPOSIT = Console.ReadLine();
                        Console.Write(StandardMessage.AskDepositAmount());
                        
                        try
                        {
                            int amount = Convert.ToInt32(Console.ReadLine());
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
                    case Choices.MainChoice.Login:
                        Console.Clear();
                        string ID_LOGIN, PASS_LOGIN;
                        try
                        {
                            
                            Console.WriteLine("1) Bankstaff Login\n2) Customer Login");
                            int option = Convert.ToInt32(Console.ReadLine());
                            if (option == 1)
                            {
                                Console.Clear();
                                Console.Write(StandardMessage.AskID());
                                ID_LOGIN = Console.ReadLine();
                                Console.Write(StandardMessage.AskPassword());
                                PASS_LOGIN = Console.ReadLine();
                                Console.Clear();
                                if(bankService.AuthenticateStaff(ID_LOGIN, PASS_LOGIN))
                                {
                                    bool e = false;
                                    while (!e)
                                    {
                                        Console.Clear();
                                        Console.Write(StandardMessage.StaffLoginChoice());
                                        Choices.StaffLoginChoice staffLoginChoice = (Choices.StaffLoginChoice)Enum.Parse(typeof(Choices.StaffLoginChoice), Console.ReadLine());
                                        switch (staffLoginChoice)
                                        {
                                            case Choices.StaffLoginChoice.CreateAccount:
                                                Console.Clear();
                                                Console.Write(StandardMessage.AskName());
                                                string NAME_CREATEACCOUNT = Console.ReadLine();
                                                Console.Write(StandardMessage.AskPassword());
                                                string PASS_CREATEACCOUNT = Console.ReadLine();
                                                string ID_CREATEACCOUNT = bankService.CreateCustomerAccount(NAME_CREATEACCOUNT, PASS_CREATEACCOUNT);
                                                Console.Clear();
                                                Console.WriteLine($"Bank account created with:\nAccount ID: {ID_CREATEACCOUNT}\nPassword:{PASS_CREATEACCOUNT}");
                                                Console.ReadLine();
                                                break;

                                            case Choices.StaffLoginChoice.UpdateAccount:
                                                Console.Clear();
                                                Console.Write(StandardMessage.UpdateCustomerAccount());
                                                Choices.UpdateCustomerAccountChoice updateCustomerAccountLoginChoice = (Choices.UpdateCustomerAccountChoice)Enum.Parse(typeof(Choices.UpdateCustomerAccountChoice), Console.ReadLine());
                                                switch (updateCustomerAccountLoginChoice)
                                                {
                                                    case Choices.UpdateCustomerAccountChoice.UpdateName:
                                                        Console.Clear();
                                                        Console.Write(StandardMessage.AskID());
                                                        string CustomerAccountID =  Console.ReadLine();
                                                        Console.Write(StandardMessage.AskName());
                                                        string NewName = Console.ReadLine();
                                                        NewName = bankService.UpdateCustomerName(CustomerAccountID,NewName);
                                                        Console.WriteLine($"Name has been updated to {NewName}");
                                                        Console.ReadLine();
                                                        break;
                                                    case Choices.UpdateCustomerAccountChoice.UpdatePassword:
                                                        Console.Clear();
                                                        Console.Write(StandardMessage.AskID());
                                                        string CustomerID = Console.ReadLine();
                                                        Console.Write(StandardMessage.AskPassword());
                                                        string NewPassword = Console.ReadLine();
                                                        NewPassword = bankService.UpdateCustomerPassword(CustomerID, NewPassword);
                                                        Console.Write($"Password has been updated to {NewPassword}");
                                                        Console.ReadLine();
                                                        break;
                                                    case Choices.UpdateCustomerAccountChoice.Back:
                                                        Console.Clear();
                                                        break;
                                                }
                                                break;
                                                case Choices.StaffLoginChoice.DeleteAccount:
                                                    Console.Clear();
                                                    Console.Write(StandardMessage.AskID());
                                                    string CustomerAccID = Console.ReadLine();
                                                    if (bankService.DeleteCustomerAccount(CustomerAccID))
                                                    {
                                                        Console.Write("Account Found and Deleted !!!");
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Account not found in records...");
                                                    }
                                                break;
                                            case Choices.StaffLoginChoice.AddCurrency:
                                                break;
                                            case Choices.StaffLoginChoice.UpdatesRTGS:
                                                break;
                                            case Choices.StaffLoginChoice.UpdatesIMPS:
                                                break;
                                            case Choices.StaffLoginChoice.UpdateoRTGS:
                                                break;
                                            case Choices.StaffLoginChoice.UpdateoIMPS:
                                                break;
                                            case Choices.StaffLoginChoice.ViewAccountTransaction:
                                                Console.Clear();
                                                Console.Write(StandardMessage.AskID());
                                                string ID = Console.ReadLine();
                                                ConsoleTable ctable = bankService.GetTransactions(ID);
                                                ctable.Write();
                                                Console.Write("\nPress Enter to exit...");
                                                Console.ReadLine();
                                                break;
                                            case Choices.StaffLoginChoice.RevertTransaction:
                                                break;
                                            case Choices.StaffLoginChoice.Logout:
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
                            }
                            else
                            {
                                Console.Clear();
                                Console.Write(StandardMessage.AskID());
                                ID_LOGIN = Console.ReadLine();
                                Console.Write(StandardMessage.AskPassword());
                                PASS_LOGIN = Console.ReadLine();
                                Console.Clear();

                                if (bankService.AuthenticateCustomer(ID_LOGIN, PASS_LOGIN))
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

                                        Choices.CustomerLoginChoice loginChoice = (Choices.CustomerLoginChoice)Enum.Parse(typeof(Choices.CustomerLoginChoice), Console.ReadLine());

                                        switch (loginChoice)
                                        {
                                            case Choices.CustomerLoginChoice.TransferMoney:
                                                Console.Write(StandardMessage.TransferAskID());
                                                string ID_TO = Console.ReadLine();
                                                Console.Write(StandardMessage.AskTransferAmount());
                                                int AMOUNT_TRANSFER = Convert.ToInt32(Console.ReadLine());
                                                Console.Clear();
                                                if (bankService.TransferAmount(ID_LOGIN, ID_TO, AMOUNT_TRANSFER))
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
                                            case Choices.CustomerLoginChoice.Withdraw:
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
                                            case Choices.CustomerLoginChoice.ShowTransactions:
                                                try
                                                {
                                                    Console.Clear();
                                                    ConsoleTable t = bankService.GetTransactions(ID_LOGIN);
                                                    t.Write();
                                                    Console.Write("\nPress Enter to exit...");
                                                    Console.ReadLine();

                                                }
                                                catch (Exception ee)
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine(StandardMessage.TransactionFetchingError() + ee.ToString());
                                                    Console.ReadLine();
                                                }
                                                break;
                                            case Choices.CustomerLoginChoice.Logout:
                                                e = true;
                                                break;
                                        }
                                    }
                                }
                                else
                                {
                                    Console.Clear();
                                    Console.Write(StandardMessage.InvalidCredentials());
                                    Console.ReadLine();
                                }
                            }
                            
                        }
                        catch
                        {
                            Console.Clear();
                            Console.WriteLine("Enter a valid ID or Password");
                        }
                        break;
                    case Choices.MainChoice.EXIT:
                        exit = true;
                        break;
                }
            }
        }

        

    }
}