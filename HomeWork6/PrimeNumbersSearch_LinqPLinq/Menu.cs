using System;
using System.Collections.Generic;
using System.Diagnostics;
using Library;

namespace PrimeNumbersSearch_LinqPLinq
{
    public static class Menu
    {
        public static void Start()
        {
            while (true)
            {
                //accept user input for range limits and the search way
                var lowerLimit = UserInput.ReadInt("\n\nEnter the lower range limit for searching primes:");
                var upperLimit = UserInput.ReadInt("\n\nEnter the upper range limit for searching primes:");
                var searchWay = UserInput.ReadInt("\n\n0 - Linq\n1 - PLinq\n\nChoose a way to search for prime numbers:", 0, 1);

                //set list of numbers in the range entered by user
                var numbers = Primes.GenerateListForPrimes(lowerLimit, upperLimit);

                //intialize and start watch
                var watch = new Stopwatch();
                watch.Start();

                //search primes in list of numbers
                var primes = searchWay == 0 ? Primes.SearchByLinq(numbers) : Primes.SearchByPLinq(numbers);
                watch.Stop();

                //print results
                Console.WriteLine($"\n\n\nFound {primes.Count} primes in the range [{lowerLimit}, {upperLimit}] in {watch.Elapsed}.");

                //ask user whether to continue
                if (UserInput.ReadBoolOneTime("\n\n\n0 - no, else - yes\nContinue?", "0"))
                    return;
            }
        }
    }
}
