using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectEuler
{
    class PalindromeGenerator
    {
        public long UpperLimit { get; }

        public PalindromeGenerator(long upperLimit)
        {
            //Digits = digits;
            //MaximumValue = (int)Math.Pow(10, Digits) - 1;
           // MinimumValue = (int)Math.Pow(10, Digits - 1);
            UpperLimit = upperLimit;
        }
        //empty list to store generated palindromes
        List<int> palindromes = new List<int>();
        
        /// <summary>
        /// Creates an unsorted list of palindromic numbers up to the UpperLimit specified by user
        /// This method takes its parameters from the GeneratePalindromes method.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="b"></param>
        /// <param name="isOdd"></param>
        /// <returns></returns>
        static int CreatePalindrome(int input, int b,
                        int isOdd)
        {
            int n = input;
            int palindrome = input;

            // checks if number of digits is odd
            // or even if odd then neglect the
            // last digit of input in finding reverse
            // as in case of odd number of digits
            // middle element occur once
            if (isOdd == 1)
                n /= b;

            // Creates palindrome by appending reverse of number to itself
            while (n > 0)
            {
                palindrome = palindrome * b + (n % b);
                n /= b;
            }
            return palindrome;
        }

        /// <summary>
        /// Generates a list of palindromes below an UpperLimit specified by user
        /// </summary>
        public void GeneratePalindromes()
        {
            int number;

            //This loop will run twice; j = 0 makes the number of digits in the palindrome even
            //j = 1 makes the number of digits in the palindrome odd
            for (int j = 0; j < 2; j++)
            {

                //Generates all the palindromic numbers less than the UpperLimit
                //Adds each palindrome to the list of palindromes
                int i = 1;
                while ((number = CreatePalindrome(i, 10,
                                            j % 2)) < UpperLimit)
                {
                    palindromes.Add(number);
                    i++;
                }

            }
            //sorts the list of palindromes
            List<int> sorted = new List<int>(palindromes);
            sorted.Sort();
            Console.WriteLine($"There are {palindromes.Count} palindromic numbers less than {UpperLimit}.");
            Console.WriteLine($"The largest palindromic number that is less than {UpperLimit} is {sorted[sorted.Count - 1]}");
        }
       
    }
}
