using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectEuler
{
    //Problem 4
    class LargestPalindromeProduct
    { 
        public long Digits { get; }
        public long MaximumValue { get; }
        public long MinimumValue { get; }
        public LargestPalindromeProduct()
        {
            Console.Write("Enter the desired number of digits for the factors: ");
            Digits = long.Parse(Console.ReadLine());
            MaximumValue = (long)Math.Pow(10, Digits) - 1;
            MinimumValue = (long)Math.Pow(10, Digits - 1);
        }

        public void LargestPalindrome()
        {
            long largestPalindrome = 0;
            for (long i = MaximumValue; i >= MinimumValue; i--)
            {
                string stem = i.ToString();
                char[] tempArray = stem.ToCharArray();
                Array.Reverse(tempArray);
                string reverse = new string(tempArray);
                string pal = stem + reverse;
                long palindrome = long.Parse(pal);
                for (long j = MaximumValue; j >= MinimumValue; j--)
                {
                    if (palindrome > largestPalindrome && palindrome % j == 0 && (palindrome / j).ToString().Length == Digits)
                    {
                        largestPalindrome = palindrome;
                        Console.WriteLine($"The largest palindrome that is the product of two {Digits}-digit numbers is: {largestPalindrome}");
                        return;
                    }
                }
            }
        }
        
    }
}
