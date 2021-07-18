using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectEuler
{
    class Program
    {
        static void Main(string[] args)
        {
            SumSquareDifference ssd = new SumSquareDifference();
            ssd.Compute();
            Console.WriteLine();
            Console.WriteLine("More Efficient Method");
            ssd.ComputeEfficient();
        }
    
    }
}
