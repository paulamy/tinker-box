using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectEuler
{
    //Problem 1
    public class MultiplesOf3And5
    {
        public int MaxValue { get; set; }

        public MultiplesOf3And5()
        {
            Console.WriteLine("If we list all the natural numbers below 10 that are multiples of 3 or 5, we get 3, 5, 6 and 9.");
            Console.WriteLine("The sum of these multiples is 23.");
            Console.WriteLine("Find the sum of all the multiples of 3 or 5 below a specified input."); 
            Console.Write("\nSpecify the input: ");
            MaxValue = int.Parse(Console.ReadLine());
            Console.WriteLine($"The sum is: {Sum()}");
        }
        public int Sum()
        {
            int sum = 0;
            for (int i = 0; i < MaxValue; i++)
            {
                if (i % 3 == 0 || i % 5 == 0)
                {
                    sum += i;
                }
            }
            return sum;
        }
    }
}
