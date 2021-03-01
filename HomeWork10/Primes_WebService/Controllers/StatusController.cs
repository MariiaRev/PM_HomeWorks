using System.Net;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Primes_WebService.Controllers
{
    [Route("/")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly ILogger<StatusController> _logger;

        public StatusController(ILogger<StatusController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public IActionResult Status()
        {
            var title = "This is Homework 9 Task 1. Prime Numbers Web Service made by Mariia Revenko";

            _logger.LogInformation($"{nameof(StatusController)}: Status request.\n{title}\nReturn {HttpStatusCode.OK}.");

            return Ok(title.ToString());
        }
    }
}
