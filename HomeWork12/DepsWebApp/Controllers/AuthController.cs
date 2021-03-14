using DepsWebApp.Filters;
using DepsWebApp.Models;
using DepsWebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace DepsWebApp.Controllers
{
    /// <summary>
    /// Authorization controller.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [TypeFilter(typeof(CustomExceptionFilter))]
    [SwaggerTag("This is the authorization controller.")]
    public class AuthController: ControllerBase
    {
        private readonly IAuthService _authService;

        /// <summary>
        /// Constructor with DI.
        /// </summary>
        /// <param name="authService"><see cref="AuthInMemoryService"/> service.</param>
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="user">User with login and password accepted from request body.</param>
        /// <returns>
        /// <see cref="OkObjectResult"/> if <paramref name="user"/> model is valid and <paramref name="user"/> was registered.
        /// <see cref="ConflictObjectResult"/> if <paramref name="user"/> model is valid and <see cref="User.Login"/> already exists.
        /// <see cref="BadRequestObjectResult"/> with explanations of the model's invalidity if <paramref name="user"/> model is invalid.</returns>
        [AllowAnonymous]
        [HttpPost("[action]")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Dictionary<string, object>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> RegisterAsync([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var registered = await _authService.RegisterAsync(user);
            
            if (registered)
            {
                return Ok();
            }
            
            return Conflict("User already exists. Please try another login.");
        }
    }
}
