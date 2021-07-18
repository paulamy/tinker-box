using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectEuler
{
    //Problem 2
    public class EvenFibonacciNumbers
    {
        public int MaxValue { get; set; }

        public EvenFibonacciNumbers(int maxValue)
        {
            MaxValue = maxValue;
        }
        public int Sum()
        {
            int sum = 0;
            List<int> sequence = new List<int>();
            int a = 1;
            int b = 2;
            int c = 0;
            sequence.Add(1);
            sequence.Add(2);
            while (c<MaxValue)
            {
                c = a + b;
                sequence.Add(c);
                a = b;
                b = c; 
            }
            foreach (int term in sequence)
            {
                if (term % 2 == 0)
                {
                    sum += term;
                }
            }
            return sum;
        }
    }
}
