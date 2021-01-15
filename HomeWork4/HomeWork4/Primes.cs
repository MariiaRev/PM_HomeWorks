using System;
using System.Collections.Generic;

namespace HomeWork4
{
    public static class Primes
    {
        public static List<int> FindPrimesInRange(int a, int b)
        {
            List<int> primes = new List<int>();
            bool isPrime;

            for (int i = a; i < b; i++)
            {
                if (i >= 2)
                {
                    isPrime = true;
                    for (int j = 2; j <= i / 2; j++)                              // search for divisors from 2 to i/2 
                    {
                        if (i % j == 0)
                        {
                            isPrime = false;
                            break;
                        }
                    }
                    if (isPrime)
                        primes.Add(i);
                }
            }
            return primes;
        }
    }
}
