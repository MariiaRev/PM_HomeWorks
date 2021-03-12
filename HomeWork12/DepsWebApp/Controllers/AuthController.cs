using DepsWebApp.Filters;
using DepsWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using Swashbuckle.AspNetCore.Annotations;

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
        /// <summary>
        /// Register a new user.
        /// </summary>
        /// <param name="user">User with login and password accepted from request body.</param>
        /// <exception cref="NotImplementedException"> while the action is not implemented.</exception>
        [HttpPost("[action]")]
        public IActionResult Register([FromBody] User user)
        {
            throw new NotImplementedException();
        }
    }
}
