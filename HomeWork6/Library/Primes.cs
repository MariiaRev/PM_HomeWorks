using System.Collections.Generic;
using System.Linq;

namespace Library
{
    public static class Primes
    {
        public static List<int> SearchByLinq(List<int> numbers)
        {
            return numbers.Where(n => n >= 2 && Enumerable.Range(2, n - 2).All(divisor => n % divisor != 0)).ToList();
        }

        public static List<int> SearchByPLinq(List<int> numbers)
        {
            return numbers.AsParallel()
                          .AsOrdered()
                          .Where(n => n >= 2 && Enumerable.Range(2, n - 2).All(divisor => n % divisor != 0))
                          .ToList();
        }

        public static List<int> GenerateListForPrimes(int start, int end)
        {
            if (end < start || (start < 2 && end < 2))
                return new List<int>();

            if (start < 2)
            {
                return Enumerable.Range(2, end - 1).ToList();
            }
            
            return Enumerable.Range(start, end - start + 1).ToList();
        }
    }
}
