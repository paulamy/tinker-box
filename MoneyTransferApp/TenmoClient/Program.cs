using System;
using System.Collections.Generic;
using TenmoClient.Models;

namespace TenmoClient
{
    class Program
    {
        private static readonly MenuService menuService = new MenuService();

        static void Main()
        {
            Run();
        }
        private static void Run()
        {
            bool programContinue = true; // Changed by MenuSelection, determines if we return to login screen
            while (programContinue)
            {
                menuService.LoginMenu();
                menuService.MenuSelection();
            }
        }

        
    }
}
