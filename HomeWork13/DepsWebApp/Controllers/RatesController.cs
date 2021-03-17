using DepsWebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using System.Threading.Tasks;

namespace DepsWebApp.Controllers
{
    /// <summary>
    /// Rates controller.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [SwaggerTag("This is the rates controller.")]
    [Authorize]
    public class RatesController : ControllerBase
    {
        private readonly IRatesService _rates;

        /// <summary>
        /// Construstor with DI.
        /// </summary>
        public RatesController(
            IRatesService rates)
        {
            _rates = rates;
        }

        /// <summary>
        /// Gets exchange amount from <paramref name="srcCurrency"/> to <paramref name="dstCurrency"/>.
        /// If <paramref name="amount"/> is not set, then it is considered that it is equal to one.
        /// </summary>
        /// <param name="srcCurrency">The currency from which to make the exchange.</param>
        /// <param name="dstCurrency">The currency to which to make the exchange.</param>
        /// <param name="amount">The amount of the exchange.</param>
        /// <returns>
        /// The <see cref="HttpStatusCode.OK"/> and the exchanged amount 
        /// if <paramref name="srcCurrency"/> and <paramref name="dstCurrency"/> currencies are valid,
        /// the <see cref="HttpStatusCode.OK"/> and the input <paramref name="amount"/> 
        /// if <paramref name="srcCurrency"/> and <paramref name="dstCurrency"/> are equal,
        /// the <see cref="HttpStatusCode.BadRequest"/> and an error message  otherwise.
        /// </returns>
        [HttpGet("{srcCurrency}/{dstCurrency}")]
        [ProducesResponseType(typeof(decimal), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Get(string srcCurrency, string dstCurrency, decimal? amount)
        {
            var exchange = await _rates.ExchangeAsync(srcCurrency, dstCurrency, amount ?? decimal.One);
            if (!exchange.HasValue)
            {
                return BadRequest("Invalid currency code");
            }
            return Ok(exchange.Value.DestinationAmount);
        }
    }
}
