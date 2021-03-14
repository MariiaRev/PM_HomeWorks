using DepsWebApp.Models;
using System.Threading.Tasks;

namespace DepsWebApp.Services
{
    /// <summary>
    /// Authorization service abstraction.
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Registers user and assigns unique account id.
        /// </summary>
        /// <param name="user">User with login and password.</param>
        /// <returns>
        /// Returns <c>true</c> if user was registered or 
        /// <c>false</c> if user was not registered because login already exists.</returns>
        Task<bool> RegisterAsync(User user);
    }
}
