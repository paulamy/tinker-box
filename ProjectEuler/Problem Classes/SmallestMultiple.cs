using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectEuler
{
    //Problem 5
    class SmallestMultiple
    {
        public long MaxFactor { get; }

        public SmallestMultiple()
        {
            Console.Write("Enter the maximum factor: ");
            MaxFactor = long.Parse(Console.ReadLine());
        }


        public void GetSmallestMultiple()
        {
            List<long> primes = new List<long>();
            long multiple = 1;
            int count2 = 0;
            int count3 = 0;
            for (int i = 2; i <= MaxFactor; i++)
            {
                multiple *= i;
            }
            while (multiple%2==0)
            {
                multiple /= 2;
                count2++;
            }
            while (multiple%3==0)
            {
                multiple /= 3;
                count3++;
            }
            Console.WriteLine(multiple);
        }

    }
}
