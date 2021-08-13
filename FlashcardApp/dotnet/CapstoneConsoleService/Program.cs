using System;

namespace CapstoneConsoleService
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
                menuService.MenuSelection();
            }
        }
    }
    
}
