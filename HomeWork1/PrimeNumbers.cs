using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWork1
{
    class PrimeNumbers
    {
        public static List<int> SearchStandard(int a, int b)            //standard way of searching primes
        {
            List<int> primes = new List<int>();
            bool flag = false;

            for(int i = a; i <= b; i++)
            {
                flag = false;
                for(int j=2; j<= i/2; j++)                              // search for divisors from 2 to i/2 
                {
                    if (i % j == 0)
                    {
                        flag = true;
                        break;
                    }
                }
                if(!flag)
                    primes.Add(i);
            }
            return primes;
        }

        public static List<int> SearchOptimized(int a, int b)            //optimized way of searching primes
        {
            List<int> primes = new List<int>();
            bool flag = false;

            for (int i = a; i <= b; i++)
            {
                flag = false;
                for (int j = 2; j <= Math.Sqrt(i); j++)                  //search for divisors from 2 to sqrt(i)
                {
                    if (i % j == 0)
                    {
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                    primes.Add(i);
            }
            return primes;
        }
    }
}
