using CapstoneClient;
using CapstoneClient.API_Services;
using CapstoneClient.Models;
using System;
using System.Collections.Generic;
using System.Text;


namespace CapstoneConsoleService
{
    public class MenuService
        {
        private static ConsoleService consoleService = new ConsoleService();

            public bool MenuSelection()
            {
                int menuSelection = -1;
                while (menuSelection != 0)
                {
                Console.Clear();
                Console.WriteLine("Welcome to the Flashcard deck generator.");
                Console.WriteLine("Please select the type of deck to generate: ");
                Console.WriteLine("1: Question Cards (Trivia API) ");
                Console.WriteLine("2: Definition Cards (Dictionary API) ");
                Console.WriteLine("3: Image Cards (Met Museum API)");
                Console.WriteLine("0: Exit");
                Console.WriteLine("---------");
                Console.Write("Please choose an option: ");

                if (!int.TryParse(Console.ReadLine(), out menuSelection))
                {
                    Console.WriteLine("Invalid input. Please enter only a number.");
                    menuSelection = -1;
                }
                else if (menuSelection == 0)
                {
                    Console.WriteLine("Exiting the Application.");
                    Environment.Exit(0);
                }
                else if (menuSelection == 1)
                {
                    //Trivia Question Deck
                    consoleService.TriviaDeckBuilder();
                    Console.WriteLine("Press any key to continue.");
                    Console.Read();
                }
                else if (menuSelection == 2)
                {
                    //Definition Question Deck
                    consoleService.DefinitionDeckBuilder();
                    Console.WriteLine("Press any key to continue.");
                    Console.Read();
                }
                else if (menuSelection == 3)
                {
                    //Image Identification Deck
                    consoleService.ImageDeckBuilder();
                    Console.WriteLine("Press any key to continue.");
                    Console.Read();
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
