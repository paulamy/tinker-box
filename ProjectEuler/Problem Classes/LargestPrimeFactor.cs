using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectEuler
{
    //Problem 3
    public class LargestPrimeFactor
    {
        public long Value { get; set; }
        
        public LargestPrimeFactor(long value)
        {
            Value = value;
        }
        public long PrimeFactors()
        {
            //a list of the prime factors of the given value
            List<long> primeFactors = new List<long>();
            //a placeholder variable to do our computation
            //initialize as the given value
            long n = Value;
            //if n is even, add 2 to the list of factors and divide by 2 until its odd
            while (n%2 == 0)
            {
                primeFactors.Add(2);
                n /= 2;
            }
            //once n is odd, loop through the odd numbers up to the squareroot of n
            //for any number composite number, n, there is at least one prime factor
            //that is less than or equal to the square root of n
            for (long i = 3; i<=Math.Sqrt(n); i+=2)
            {
                //if i is a factor of n, add it to the list and divide by i
                //this will repeat until n is 1, or a prime number
                while (n%i == 0)
                {
                    primeFactors.Add(i);
                    n /= i;
                }
            }
            //if n is a prime number at this point, then add it to the list
            if (n>2)
            {
                primeFactors.Add(n);
            }
            //return the largest prime factor
            return primeFactors[primeFactors.Count-1];
        }
        /// <summary>
        /// This is the direct search factorization method or trial division. 
        /// works best for small numbers; unwieldy for larger values
        /// </summary>
        /// <returns>The largest prime factor of a given value</returns>
        /*public long PrimeFactors()
        {
            List<long> primeFactors = new List<long>();
            List<long> factors = new List<long>();
            int prime = 0;
            for (long i = 1; i <= Value/2; i++)
            {
                if (Value % i == 0)
                {
                    factors.Add(i);
                }
            }    
            foreach (long num in factors)
            {
                prime = 0;
                for (int i = 1; i <= num; i++)
                {
                    if(num%i == 0)
                    {
                        prime++;
                    }
                }
                if (prime == 2)
                {
                    primeFactors.Add(num);
                }
            }    
            return primeFactors[primeFactors.Count-1]; 
        }*/
    }
}
