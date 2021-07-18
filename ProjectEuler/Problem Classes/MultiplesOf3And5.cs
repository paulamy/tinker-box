using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectEuler
{
    //Problem 1
    public class MultiplesOf3And5
    {
        public int MaxValue { get; set; }

        public MultiplesOf3And5(int maxValue)
        {
            MaxValue = maxValue;
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
