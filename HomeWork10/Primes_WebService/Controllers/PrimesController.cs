using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Primes_WebService.Services;

namespace Primes_WebService.Controllers
{
    [Route("/primes")]
    [ApiController]
    public class PrimesController : ControllerBase
    {
        private readonly IPrimesService _primesService;
        private readonly ILogger<PrimesController> _logger;
        public PrimesController(
            IPrimesService primesService,
            ILogger<PrimesController> logger)
        {
            _primesService = primesService;
            _logger = logger;
        }

        [HttpGet("{number}")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> CheckIfIsPrime(int number)
        {
            var isPrime = await _primesService.IsPrimeAsync(number);
            _logger.LogInformation($"{nameof(PrimesController)}: Request to check the primeness of the number {number}.");

            if (isPrime)
            {
                var message = $"The number {number} is prime.";
                _logger.LogInformation($"{nameof(PrimesController)}: {message} Return {HttpStatusCode.OK}.");

                return Ok(message);
            }
            else
            {
                var message = $"The number {number} is not prime.";
                _logger.LogInformation($"{nameof(PrimesController)}: {message} Return {HttpStatusCode.NotFound}.");

                return NotFound(message);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetPrimesFromRange([FromQuery]int? from, [FromQuery]int? to)
        {
            _logger.LogInformation($"{nameof(PrimesController)}: Request to check the primeness of numbers in range.");

            if (from == null || to == null)
            {
                _logger.LogInformation($"{nameof(PrimesController)}: Invalid range. Return {HttpStatusCode.BadRequest}.");
                return BadRequest();
            }
            else
            {
                var primes = await _primesService.GetPrimesInRangeAsync((int)from, (int)to);
                var responseContent = $"[{string.Join(", ", primes)}]";
                _logger.LogInformation($"{nameof(PrimesController)}: Prime numbers in the range [{from}, {to}]: " +
                    $"{responseContent}. Return {HttpStatusCode.OK}.");

                return Ok(responseContent);
            }
        }
    }
}
