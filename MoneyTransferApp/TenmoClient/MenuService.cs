using System;
using System.Collections.Generic;
using System.Text;
using TenmoClient.Models;

namespace TenmoClient
{
    public class MenuService
    {
        private static ConsoleService consoleService = new ConsoleService();
        private static readonly AuthService authService = new AuthService();
        public void LoginMenu()
        {
            int menuSelection = -1;
            while (menuSelection != 1 && menuSelection != 2)
            {
                WriteLoginMenu();
                if (!int.TryParse(Console.ReadLine(), out menuSelection))
                {
                    Console.WriteLine("Invalid input. Please enter only a number.");
                }

                else if (menuSelection == 1)
                {
                    while (!UserService.IsLoggedIn()) //will keep looping until user is logged in
                    {
                        authService.AttemptLogin(consoleService.PromptForLogin());
                        consoleService.ResetAPI();              // Resets API to pick up new JWT token
                    }
                }
                else if (menuSelection == 2)
                {
                    while (!authService.Register(consoleService.PromptForLogin())) ; //will keep looping until user is registered
                    Console.WriteLine("\nRegistration successful. You can now log in.");
                    menuSelection = -1; //reset outer loop to allow choice for login
                }
                else
                {
                    Console.WriteLine("Invalid selection.");
                }
            }
        }
        public void WriteLoginMenu()
        {
            Console.WriteLine("Welcome to TEnmo!");
            Console.WriteLine("1: Login");
            Console.WriteLine("2: Register");
            Console.Write("Please choose an option: ");
        }
        public bool MenuSelection()
        {
            int menuSelection = -1;
            while (menuSelection != 0)
            {
                Console.Clear();
                Console.WriteLine("Welcome to TEnmo! Please make a selection: ");
                Console.WriteLine("1: View your current balance");
                Console.WriteLine("2: View your past transfers");
                Console.WriteLine("3: View your pending requests");
                Console.WriteLine("4: Send TE bucks");
                Console.WriteLine("5: Request TE bucks");
                Console.WriteLine("6: Log in as different user");
                Console.WriteLine("0: Exit");
                Console.WriteLine("---------");
                Console.Write("Please choose an option: ");

                if (!int.TryParse(Console.ReadLine(), out menuSelection))
                {
                    Console.WriteLine("Invalid input. Please enter only a number.");
                    menuSelection = -1;
                }
                else if (menuSelection == 1)
                {
                    consoleService.GetBalance();
                }
                else if (menuSelection == 2)
                {
                    bool requestExit = false;
                    while (!requestExit)
                    {
                        Console.Clear();
                        Console.WriteLine("--------------------------------------------");
                        Console.WriteLine("Transfers");
                        Console.WriteLine("ID          From / To                 Amount");
                        Console.WriteLine("--------------------------------------------");
                        consoleService.GetTransfers();
                        requestExit = consoleService.GetTransferDetails();
                    }
                }
                else if (menuSelection == 3)
                {
                    bool requestExit = false;
                    while (!requestExit)
                    {
                        Console.Clear();
                        Console.WriteLine("---------------------------------------------");
                        Console.WriteLine("Pending Transfers");
                        Console.WriteLine("ID               To                    Amount");
                        Console.WriteLine("---------------------------------------------");
                        if (consoleService.GetPendingTransfers())
                        {
                            Console.WriteLine("No Pending Transfers. Press any key to continue.");
                            Console.ReadKey();
                            requestExit = true;
                        }
                        else
                        {
                            requestExit = consoleService.ReviewPendingTransfers();
                        }
                    }
                }
                else if (menuSelection == 4)
                {
                    consoleService.PrintAccounts();
                    consoleService.SendTEBucks();
                }
                else if (menuSelection == 5)
                {
                    consoleService.PrintAccounts();
                    consoleService.RequestTEBucks();
                }
                else if (menuSelection == 6)
                {
                    Console.WriteLine("");
                    UserService.SetLogin(new API_User()); //wipe out previous login info
                    return true; //return to entry point
                }
                else
                {
                    Console.WriteLine("\n\nError: Please select valid menu item. Press any key to continue.");
                    Console.ReadKey();
                }
            }
            return false;
        }
    }
}
