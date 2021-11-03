using System;
using System.Collections.Generic;
using BankApp.Service;
using ConsoleTables;

namespace BankApp.CLI
{

    public partial class Program //PARTIAL CLASSES
    {
        static void Main(string[] args)
        {
            bool exit = false;

            BankService bankService = new BankService();
            println(bankService.init());
            Console.ReadLine();

            while (!exit)
            {
                Console.Clear();
                try {
                    MainMenu MainMenu = (MainMenu)Enum.Parse(typeof(MainMenu), GetString(StandardMessage.WelcomeMenu)); // TRY PARSE
                    switch (MainMenu)
                    {
                        case MainMenu.Deposit:
                            Console.Clear();
                            string ID_DEPOSIT = GetString(StandardMessage.AskAccountID);

                            string currency = GetString("Enter the first 3 letter if the currency: ");

                            int amount = GetNumber(StandardMessage.AskDepositAmount);
                            string NAME_DEPOSIT = bankService.DepositAmount(ID_DEPOSIT, amount, currency);
                            Console.Clear();
                            println($"{amount}₹ have been deposited into {NAME_DEPOSIT} Account");
                            GetString("");
                            break;


                        case MainMenu.Login:
                            Console.Clear();
                            string ID_LOGIN, PASS_LOGIN;

                            int option = GetNumber("1) Bankstaff Login\n2) Customer Login\n\nEnter Your Choice: ");

                            Console.Clear();
                            ID_LOGIN = GetString(StandardMessage.AskAccountID);

                            PASS_LOGIN = GetString(StandardMessage.AskPassword);
                            Console.Clear();

                            if (option == 1)
                            {
                                if (bankService.AuthenticateStaff(ID_LOGIN, PASS_LOGIN))
                                {
                                    bool e = false;
                                    while (!e)
                                    {
                                        Console.Clear();
                                        StaffLoginMenu StaffLoginMenu = (StaffLoginMenu)Enum.Parse(typeof(StaffLoginMenu), GetString(StandardMessage.StaffLoginMenu));
                                        switch (StaffLoginMenu)
                                        {
                                            case StaffLoginMenu.CreateAccount:
                                                Console.Clear();
                                                string NAME_CREATEACCOUNT = GetString(StandardMessage.AskName);
                                                string PASS_CREATEACCOUNT = GetString(StandardMessage.AskPassword);
                                                string ID_CREATEACCOUNT = bankService.CreateCustomerAccount(NAME_CREATEACCOUNT, PASS_CREATEACCOUNT);
                                                Console.Clear();
                                                println($"Bank account created with:\nAccount ID: {ID_CREATEACCOUNT}\nPassword:{PASS_CREATEACCOUNT}");
                                                GetString("");
                                                break;

                                            case StaffLoginMenu.UpdateAccount:
                                                Console.Clear();
                                                UpdateCustomerAccountMenu updateCustomerAccountLoginChoice = (UpdateCustomerAccountMenu)Enum.Parse(typeof(UpdateCustomerAccountMenu), GetString(StandardMessage.UpdateCustomerAccount));
                                                switch (updateCustomerAccountLoginChoice)
                                                {
                                                    case UpdateCustomerAccountMenu.UpdateName:
                                                        Console.Clear();
                                                        string CustomerAccountID = GetString(StandardMessage.AskAccountID);
                                                        string NewName = GetString(StandardMessage.AskName);
                                                        NewName = bankService.UpdateCustomerName(CustomerAccountID, NewName);
                                                        println($"Name has been updated to {NewName}");
                                                        GetString("");
                                                        break;
                                                    case UpdateCustomerAccountMenu.UpdatePassword:
                                                        Console.Clear();
                                                        string CustomerID = GetString(StandardMessage.AskAccountID);
                                                        string NewPassword = GetString(StandardMessage.AskPassword);
                                                        NewPassword = bankService.UpdateCustomerPassword(CustomerID, NewPassword);
                                                        print($"Password has been updated to {NewPassword}");
                                                        GetString("");
                                                        break;
                                                    case UpdateCustomerAccountMenu.Back: //USE DEFAULT
                                                        Console.Clear();
                                                        break;
                                                }
                                                break;
                                            case StaffLoginMenu.DeleteAccount:
                                                Console.Clear();
                                                string CustomerAccID = GetString(StandardMessage.AskAccountID);
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
                                                Console.Clear();
                                                string currencyName = GetString("Enter 3 letter name of the new currency: ");
                                                float rate = GetNumber($"Enter the number of rupees per one {currencyName}: ");
                                                bankService.AddCurrency(currencyName,rate);
                                                break;
                                            case StaffLoginMenu.UpdatesRTGS:
                                                string bankID = GetString("Enter Bank ID:");
                                                int NewsRTGS = GetNumber("Enter New sRTGS value: ");
                                                int temp = bankService.UpdatesRTGS(NewsRTGS,bankID);
                                                print($"sRTGS updated to {temp}");
                                                Console.ReadLine();
                                                break;
                                            case StaffLoginMenu.UpdatesIMPS:
                                                bankID = GetString("Enter Bank ID:");
                                                int NewsIMPS = GetNumber("Enter New sRTGS value: ");
                                                temp = bankService.UpdatesRTGS(NewsIMPS, bankID);
                                                print($"sRTGS updated to {temp}");
                                                Console.ReadLine();
                                                break;
                                            case StaffLoginMenu.UpdateoRTGS:
                                                bankID = GetString("Enter Bank ID:");
                                                int NewoRTGS = GetNumber("Enter New sRTGS value: ");
                                                temp = bankService.UpdatesRTGS(NewoRTGS, bankID);
                                                print($"sRTGS updated to {temp}");
                                                Console.ReadLine();
                                                break;
                                            case StaffLoginMenu.UpdateoIMPS:
                                                bankID = GetString("Enter Bank ID:");
                                                int NewoIMPS = GetNumber("Enter New sRTGS value: ");
                                                temp = bankService.UpdatesRTGS(NewoIMPS, bankID);
                                                print($"sRTGS updated to {temp}");
                                                Console.ReadLine();
                                                break;
                                            case StaffLoginMenu.ViewAccountTransaction:
                                                Console.Clear();
                                                string ID = GetString(StandardMessage.AskAccountID);
                                                ConsoleTable ctable = bankService.GetTransactions(ID);
                                                ctable.Write();
                                                print("\nPress Enter to exit...");
                                                GetString("");
                                                break;
                                            case StaffLoginMenu.RevertTransaction:
                                                Console.Clear();
                                                print("Enter the TransactionID: ");
                                                string TransactionID = Console.ReadLine();
                                                if (bankService.RevertTransaction(TransactionID))
                                                {
                                                    Console.Clear();
                                                    print("Transaction successfully reverted !!!");
                                                    Console.ReadLine();
                                                }
                                                else
                                                {
                                                    Console.Clear();
                                                    print("Insufficient funds...");
                                                    Console.ReadLine();
                                                }
                                                
                                                break;
                                            case StaffLoginMenu.Logout:
                                                e = true;
                                                break;
                                            case StaffLoginMenu.ShowBankProfits:
                                                bankID = GetString("Enter BankID: ");
                                                print(bankService.ShowBankProfits(bankID));
                                                Console.ReadLine();
                                                break;
                                        }
                                    }
                                }
                                else
                                {
                                    GetString(StandardMessage.InvalidCredentials);
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


                                        CustomerLoginMenu loginChoice = (CustomerLoginMenu)Enum.Parse(typeof(CustomerLoginMenu), GetString(StandardMessage.LoginMenu));

                                        switch (loginChoice)
                                        {
                                            case CustomerLoginMenu.TransferMoney:
                                                string ID_TO = GetString(StandardMessage.TransferAskID);
                                                int AMOUNT_TRANSFER = GetNumber(StandardMessage.AskTransferAmount);
                                                Console.Clear();
                                                if (bankService.TransferAmount(ID_LOGIN, ID_TO, AMOUNT_TRANSFER))
                                                {
                                                    GetString(StandardMessage.TransactionSuccess);

                                                }
                                                else
                                                {
                                                    GetString(StandardMessage.TransactionErrorInsufficientBal);
                                                }
                                                break;
                                            case CustomerLoginMenu.Withdraw:
                                                int a = GetNumber(StandardMessage.AskWithdrawAmount);

                                                string check = bankService.WithdrawAmount(ID_LOGIN, a);
                                                if (check == "Failed")
                                                {
                                                    Console.Clear();
                                                    GetString(StandardMessage.InsuffiecientFunds);
                                                }
                                                else
                                                {
                                                    Console.Clear();
                                                    println($"{a} has been withdrawed succesfully");
                                                    GetString("");
                                                }

                                                break;
                                            case CustomerLoginMenu.ShowTransactions:
                                                Console.Clear();
                                                ConsoleTable t = bankService.GetTransactions(ID_LOGIN);
                                                t.Write();
                                                print("\nPress Enter to exit...");
                                                GetString("");
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
                                    GetString("");
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