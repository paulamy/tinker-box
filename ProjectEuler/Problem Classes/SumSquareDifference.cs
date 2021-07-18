using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectEuler
{
    //problem 6
    public class SumSquareDifference
    {
        public int FirstN { get; private set; }

        public double SumOfSquares { get; private set; } = 0;

        public double SquareOfSums { get; private set; } = 0;

        public double TheDifference { get; private set; } = 0;

        public SumSquareDifference()
        {
                Console.WriteLine("This script will comput the positive difference between");
                Console.WriteLine("the sum of the squares of the first n ingtegers");
                Console.WriteLine("and the square of the sum of the first n integers.");
                Console.Write("Please enter the value of n: ");
                FirstN = int.Parse(Console.ReadLine());  
        }

        public void ComputeEfficient()
        {
            SumOfSquares = (1 / 6.0) *(2* Math.Pow(FirstN, 3) + 3 * Math.Pow(FirstN, 2) + FirstN);
            SquareOfSums = Math.Pow(((FirstN / 2)*( FirstN + 1)), 2);
            TheDifference = Math.Abs(SumOfSquares - SquareOfSums);
            Console.WriteLine($"The first {FirstN} numbers.");
            Console.WriteLine($"The sum of the squares is {SumOfSquares}.");
            Console.WriteLine($"The square of the sums is {SquareOfSums}.");
            Console.WriteLine($"The difference is {TheDifference}.");
        }

        public double SumSquares()
        {
            for (int i = 1; i<= FirstN; i++)
            {
                SumOfSquares += i * i;
            }
            return SumOfSquares;
        }

        public double SquareSums()
        {
            for (int i = 1; i <= FirstN; i++)
            {
                SquareOfSums += i;
            }
            SquareOfSums *= SquareOfSums;
            return SquareOfSums;
        }

        public double Difference()
        {
            TheDifference = Math.Abs(SumOfSquares - SquareOfSums);
            return TheDifference;
        }

        public void Compute()
        {
            SumSquares();
            SquareSums();
            Difference();
            Console.WriteLine($"The first {FirstN} numbers.");
            Console.WriteLine($"The sum of the squares is {SumOfSquares}.");
            Console.WriteLine($"The square of the sums is {SquareOfSums}.");
            Console.WriteLine($"The difference is {TheDifference}.");
        }

       
    }
}
