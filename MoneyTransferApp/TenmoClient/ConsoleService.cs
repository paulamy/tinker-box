using System;
using System.Collections.Generic;
using System.Net.Http;
using TenmoClient.Models;

namespace TenmoClient
{
    public class ConsoleService
    {
        private static readonly AuthService authService = new AuthService();
        private static ApiService apiService = new ApiService();
        private static readonly MenuService menuService = new MenuService();
        /// <summary>
        /// Prompts for transfer ID to view, approve, or reject
        /// </summary>
        /// <param name="action">String to print in prompt. Expected values are "Approve" or "Reject" or "View"</param>
        /// <returns>ID of transfers to view, approve, or reject</returns>
        public LoginUser PromptForLogin()
        {
            Console.Write("Username: ");
            string username = Console.ReadLine();
            string password = GetPasswordFromConsole("Password: ");

            LoginUser loginUser = new LoginUser
            {
                Username = username,
                Password = password
            };
            return loginUser;
        }
        public void GetBalance()
        {
            Console.Clear();
            Console.WriteLine($"Your current account balance is: {apiService.GetBalance():C2}");
            Console.WriteLine("\n\nPress any key to continue");
            Console.ReadKey();
            Console.Clear();
        }
        private string GetPasswordFromConsole(string displayMessage)
        {
            string pass = "";
            Console.Write(displayMessage);
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);

                // Backspace Should Not Work
                if (!char.IsControl(key.KeyChar))
                {
                    pass += key.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && pass.Length > 0)
                    {
                        pass = pass.Remove(pass.Length - 1);
                        Console.Write("\b \b");
                    }
                }
            }
            // Stops Receving Keys Once Enter is Pressed
            while (key.Key != ConsoleKey.Enter);
            Console.WriteLine("");
            return pass;
        }

        public bool SendTransfer(int account, decimal amount)
        {
            if (apiService.CreateTransfer(account, new Transfer
            {
                Amount = amount,
                AccountFrom = UserService.GetUserId(),
                AccountTo = account,
                TransferTypeID = 2,
                TransferStatusID = 2
            }))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        public bool RequestTransfer(int account, decimal amount)
        {
            if (apiService.CreateTransfer(account, new Transfer
            {
                Amount = amount,
                AccountFrom = account,
                AccountTo = UserService.GetUserId(),
                TransferTypeID = 1,
                TransferStatusID = 1
            }))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void PrintAccounts()
        {
            Console.Clear();
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("Users");
            Console.WriteLine("ID          Name");

            List<PublicAccount> accounts = apiService.GetAccounts();
            foreach (PublicAccount account in accounts)
            {
                if(account.UserId != UserService.GetUserId())
                {
                    Console.WriteLine(String.Format("{0,-12}{1, -20}", account.UserId, account.Username));
                } 
            }

            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("---------\n");
        }
        public void GetTransfers()
        {
            List<Transfer> complTransfers = apiService.GetTransfers();
            foreach (var transfer in complTransfers)
            {
                string toUser = "";
                string fromUser = "";
                if (UserService.GetUserId() == transfer.AccountFrom)
                {
                    toUser = apiService.GetUser(transfer.AccountTo).Username;
                }
                else if (UserService.GetUserId() == transfer.AccountTo)
                {
                    fromUser = apiService.GetUser(transfer.AccountFrom).Username;
                }
                string type = fromUser != "" ? "From: " : "  To: ";
                string otherUser = fromUser != "" ? $"{fromUser}" : $"{toUser}";
                Console.WriteLine(string.Format("{0, -12}{1, -20}{2, 12}", $"{transfer.TransferID}", type + otherUser, $"{transfer.Amount:C2}"));
            }
        }
        public bool GetPendingTransfers()
        {
            List<Transfer> complTransfers = apiService.GetPendingTransfers();
            foreach (var transfer in complTransfers)
            {
                string toUser = apiService.GetUser(transfer.AccountTo).Username;
                Console.WriteLine(string.Format("{0, -12}{1, -20}{2, 12}", $"{transfer.TransferID}", $"To: { toUser}", $"{transfer.Amount:C2}"));
            }
            return complTransfers.Count == 0;
        }

        public bool GetTransferDetails() // return true = I want to be finished with this menu
        {
            Transfer transfer = null;
            Console.Write("\n\nPlease enter transfer ID to view details (0 to cancel): ");
            if (int.TryParse(Console.ReadLine(), out int result))
            {
                if (result == 0) // if user chooses to exit
                    return true;

                transfer = apiService.GetSpecificTransfer(result);
                if (transfer == null)
                {
                    Console.WriteLine("\n\nTransfer does not exist, Press any key to continue");
                    Console.ReadKey();
                    return false;
                }
            }
            else
            {
                Console.WriteLine("\n\nInvalid input, Press any key to continue");
                Console.ReadKey();
                return false;
            }
            Console.Clear();
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("Transfer Details");
            Console.WriteLine("--------------------------------------------");
            string toUser = "";
            string fromUser = "";
            if (UserService.GetUserId() == transfer.AccountFrom)
            {
                toUser = apiService.GetUser(transfer.AccountTo).Username;
            }
            else if (UserService.GetUserId() == transfer.AccountTo)
            {
                fromUser = apiService.GetUser(transfer.AccountFrom).Username;
            }
            Console.WriteLine("ID: " + transfer.TransferID);
            Console.WriteLine("From: " + (fromUser == "" ? UserService.GetUserName() : fromUser));
            Console.WriteLine("To: " + (toUser == "" ? UserService.GetUserName() : toUser));
            Console.WriteLine("Type: " + transfer.TransferTypeDescription);
            Console.WriteLine("Status: " + transfer.TransferStatusDescription);
            Console.WriteLine($"Amount: {transfer.Amount:C2}");
            Console.WriteLine("\n\nPress any key to continue");
            Console.ReadKey();
            return false;
        }
        public bool ReviewPendingTransfers() // return true = I want to be finished with this menu
        {
            Transfer transfer = null;
            Console.Write("\n\nPlease enter transfer ID to approve/reject (0 to cancel): ");
            if (int.TryParse(Console.ReadLine(), out int result))
            {
                if (result == 0) // if user chooses to exit
                {
                    return true;
                }

                transfer = apiService.GetSpecificTransfer(result);
                if (transfer == null)
                {
                    Console.WriteLine("\n\nTransfer does not exist, Press any key to continue");
                    Console.ReadKey();
                    return false;
                }
                else if (transfer.AccountTo == UserService.GetUserId())
                {
                    Console.WriteLine("\n\nSorry, but you can't approve or reject requests that you've made.");
                    Console.WriteLine("\nPress any key to continue");
                    Console.ReadKey();
                    return false;
                }
            }
            else
            {
                Console.WriteLine("\n\nInvalid input, Press any key to continue");
                Console.ReadKey();
                return false;
            }
            Console.Clear();
            Console.WriteLine("1: Approve");
            Console.WriteLine("2: Reject");
            Console.WriteLine("0: Cancel (Transfer status will remain pending)");
            Console.WriteLine("------------------------------------------------");
            Console.Write("Please choose an option: ");
            if (int.TryParse(Console.ReadLine(), out int intResult))
            {
                if (intResult == 1 && VerifyBalance(transfer.Amount))
                {
                    transfer.TransferStatusID = 2;
                    apiService.UpdateTransferStatus(transfer.AccountFrom, transfer);
                    Console.WriteLine("Transfer Approved!");
                }
                else if (intResult == 1 && !VerifyBalance(transfer.Amount))
                {
                    Console.WriteLine("Cannot approve transfer; insufficient funds.");
                }
                else if (intResult == 2)
                {
                    transfer.TransferStatusID = 3;
                    apiService.UpdateTransferStatus(transfer.AccountFrom, transfer);
                    Console.WriteLine("Transfer Rejected!");
                }
                else if (intResult == 0)
                {
                    return true;
                }
                else
                {
                    Console.WriteLine("Error: Please select a valid option.");
                }
            }
            Console.WriteLine("\n\nPress any key to continue");
            Console.ReadKey();
            return false;
        }

        public bool VerifyUser(int id)
        {
            bool userExists = false;
            try
            {
                userExists = apiService.GetUser(id) != null;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(e.Message); 
            }
            return userExists;
        }

        public bool VerifyBalance(decimal amount)
        {
            bool hasEnough = apiService.GetBalance() >= amount;
            return hasEnough;
        }

        public void ResetAPI()

        {
            apiService = new ApiService();
        }

        public void SendTEBucks()
        {
            Console.Write("Enter ID of user you are sending to (0 to cancel): ");
            int account = 0;
            decimal amount = 0;
            if (!int.TryParse(Console.ReadLine(), out int intResult))
            {
                Console.WriteLine("Invalid input. Please enter only a number.");
            }
            else if (intResult == 0)
            {
                menuService.MenuSelection();
            }
            else if (!VerifyUser(intResult))
            {
                Console.WriteLine("Error: must be a valid user");
            }
            else if (intResult == UserService.GetUserId())
            {
                Console.WriteLine("Error: cannot send a transfer to yourself.");
            }
            else
            {

                account = intResult;
                Console.Write("Enter amount: $");
                if (!decimal.TryParse(Console.ReadLine(), out decimal decimalResult))
                {
                    Console.WriteLine("Invalid input. Please enter only a number.");

                }
                else if (decimalResult <= 0)
                {
                    Console.WriteLine("Invalid input. Transfers must be greater than 0.");
                }
                else if (!VerifyBalance(decimalResult))
                {
                    Console.WriteLine("Error: Insufficient funds to complete transfer.");
                }
                else
                {
                    amount = decimalResult;
                    bool transferSent = SendTransfer(account, amount);
                    if (transferSent)
                    {
                        Console.WriteLine("Transfer completed successfully!");
                    }
                    else if (!transferSent)
                    {
                        Console.WriteLine("Error: something went wrong with the transfer.");
                    }

                }

            }
            Console.WriteLine("\nPress any key to continue.");
            Console.ReadKey();
        }

        public void RequestTEBucks()
        {
            Console.Write("Enter ID of user you are requesting from (0 to cancel): ");
            int account = 0;
            decimal amount = 0;
            if (!int.TryParse(Console.ReadLine(), out int intResult))
            {
                Console.WriteLine("Invalid input. Please enter only a number.");
            }
            else if (intResult == 0)
            {
                menuService.MenuSelection();
            }
            else if (intResult == UserService.GetUserId())
            {
                Console.WriteLine("Error: cannot send a transfer to yourself.");
            }
            else if (!VerifyUser(intResult))
            {
                Console.WriteLine("Error: must be a valid user");
            }
            else
            {
                account = intResult;
                Console.Write("Enter amount: $");
                if (!decimal.TryParse(Console.ReadLine(), out decimal decimalResult))
                {
                    Console.WriteLine("Invalid input. Please enter only a number.");
                }
                else if (decimalResult <= 0)
                {
                    Console.WriteLine("Invalid input. Transfers must be greater than 0.");
                }
                else
                {
                    amount = decimalResult;
                    bool transferRequested = RequestTransfer(account, amount);
                    if (transferRequested)
                    {
                        Console.WriteLine("Transfer request complete. Status: Pending");
                    }
                    else if (!transferRequested)
                    {
                        Console.WriteLine("Error: something went wrong with the transfer request.");
                    }
                }
                
            }
            Console.WriteLine("\nPress any key to continue.");
            Console.ReadKey();

        }
    }
}
