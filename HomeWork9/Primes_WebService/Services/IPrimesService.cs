using System.Collections.Generic;
using System.Threading.Tasks;

namespace Primes_WebService.Services
{
    public interface IPrimesService
    {
        Task<bool> IsPrimeAsync(int number);
        Task<IEnumerable<int>> GetPrimesInRangeAsync(int from, int to);
    }
}
