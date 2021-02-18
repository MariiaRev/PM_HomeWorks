using System.Collections.Generic;
using System.Threading.Tasks;

namespace Primes_WebService.Services
{
    public class PrimesService : IPrimesService
    {
        public async Task<IEnumerable<int>> GetPrimesInRangeAsync(int from, int to)
        {
            var primes = new List<int>();
            
            for (int i = from; i <= to; i++)
            {
                if (await IsPrimeAsync(i))
                {
                    primes.Add(i);
                }
            }

            return primes;
        }

        public Task<bool> IsPrimeAsync(int number)
        {
            var isPrime = false;

            if (number > 2)
            {
                for (int i = 2; i < number; i++)
                {
                    if (number % i == 0)
                    {
                        isPrime = false;
                        break;
                    }

                    isPrime = true;
                }
            }
            if (number == 2)
            {
                isPrime = true;
            }

            return Task.FromResult(isPrime);
        }
    }
}
