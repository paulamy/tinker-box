using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectEuler
{
    public class UserInterface
    {
        public string Option1 { get; set; } = "Project Euler Problems";
        public string Option2 { get; set; } = "Math Utilities";
        public string Option3 { get; set; } = "Exit";
        public void StartUp()
        {

            bool madeSelection = false;
            string userInput = "";
            Console.WriteLine("Welcome to Project Euler");
            Console.WriteLine("You can see the problem solutions, or " +
                "view the math utilities derived from them.");
            while (!madeSelection)
            {
                Console.WriteLine("\nPlease select one of the following options:" +
                                  "\n(1) " + $"{Option1}" +
                                  "\n(2) " + $"{Option2}" +
                                  "\n(3) " + $"{Option3}");
                userInput = Console.ReadLine();
                if (userInput == "1" || userInput == "(1)")
                {
                    ProjectEulerProblemsMenu();
                    madeSelection = true;
                }
                else if (userInput == "2" || userInput == "(2)")
                {
                    MathUtilitiesMenu();
                    madeSelection = true;
                }
                else if (userInput == "3" || userInput == "(3)")
                {
                    // get outta here
                    Console.WriteLine("Goodbye!");
                    madeSelection = true;
                    return;
                    
                }
                else
                {
                    // check input for mistake and repeat loop
                    Console.WriteLine("I'm sorry, I don't have that option. Please try again.\n");
                }
                madeSelection = false;
            }
        }
        public void ProjectEulerProblemsMenu()
        {
            bool finished = false;
            string userInput;
            string option1 = "Exit";
            string option2 = "Multiples of 3 and 5";
            string option3 = "Even Fibonacci Numbers";
            string option4 = "Largest Prime Factor";
            string option5 = "Largest Palindrome Product";
            string option6 = "Smallest Multiple";
            string option7 = "Sum Square Difference";

            while (!finished)
            {
                Console.WriteLine("\nPlease select one of the following options:" +
                                  "\n(1) " + $"{option1}" +
                                  "\n(2) " + $"{option2}" +
                                  "\n(3) " + $"{option3}" +
                                  "\n(4) " + $"{option4}" +
                                  "\n(5) " + $"{option5}" +
                                  "\n(6) " + $"{option6}" +
                                  "\n(7) " + $"{option7}");
                userInput = Console.ReadLine();

                if (userInput == "1" || userInput == "(1)")
                {
                    Console.WriteLine($"############################# {option1} #############################");
                    finished = true;
                    
                }
                else if (userInput == "2" || userInput == "(2)")
                {
                    Console.WriteLine($"############################# {option2} #############################");
                    MultiplesOf3And5 run = new MultiplesOf3And5();
                    finished = true;
                }
                else if (userInput == "3" || userInput == "(3)")
                {
                    Console.WriteLine($"############################# {option3} #############################");
                    finished = true;
                }
                else if (userInput == "4" || userInput == "(4)")
                {
                    Console.WriteLine($"############################# {option4} #############################");
                    finished = true;
                }
                else if (userInput == "5" || userInput == "(5)")
                {
                    Console.WriteLine($"############################# {option5} #############################");
                    finished = true;
                }
                else if (userInput == "6" || userInput == "(6)")
                {
                    Console.WriteLine($"############################# {option6} #############################");
                    finished = true;
                }
                else if (userInput == "7" || userInput == "(7)")
                {
                    Console.WriteLine($"############################# {option7} #############################");
                    finished = true;
                }
                else
                {
                    //Check for errors and repeat loop
                    Console.WriteLine("I don't think that's one of the options; please try again.\n");
                }

            }

        }

        public void MathUtilitiesMenu()
        {
            bool finished = false;
            string userInput;
            string option1 = "Exit";
            string option2 = "empty";
            string option3 = "empty";
            string option4 = "empty";
            string option5 = "Palindrome Generator";
            string option6 = "empty";
            string option7 = "empty";

            while (!finished)
            {
                Console.WriteLine("\nPlease select one of the following options:" +
                                  "\n(1) " + $"{option1}" +
                                  "\n(2) " + $"{option2}" +
                                  "\n(3) " + $"{option3}" +
                                  "\n(4) " + $"{option4}" +
                                  "\n(5) " + $"{option5}" +
                                  "\n(6) " + $"{option6}" +
                                  "\n(7) " + $"{option7}");
                userInput = Console.ReadLine();

                if (userInput == "1" || userInput == "(1)")
                {
                    Console.WriteLine($"#############################{option1}#############################");
                    finished = true;

                }
                else if (userInput == "2" || userInput == "(2)")
                {
                    Console.WriteLine($"#############################{option2}#############################");
                    finished = true;
                }
                else if (userInput == "3" || userInput == "(3)")
                {
                    Console.WriteLine($"#############################{option3}#############################");
                    finished = true;
                }
                else if (userInput == "4" || userInput == "(4)")
                {
                    Console.WriteLine($"#############################{option4}#############################");
                    finished = true;
                }
                else if (userInput == "5" || userInput == "(5)")
                {
                    Console.WriteLine($"#############################{option5}#############################");
                    finished = true;
                }
                else if (userInput == "6" || userInput == "(6)")
                {
                    Console.WriteLine($"#############################{option6}#############################");
                    finished = true;
                }
                else if (userInput == "7" || userInput == "(7)")
                {
                    Console.WriteLine($"#############################{option7}#############################");
                    finished = true;
                }
                else
                {
                    //Check for errors and repeat loop
                    Console.WriteLine("I don't think that's one of the options; please try again.\n");
                }

            }

        }
    }
}
