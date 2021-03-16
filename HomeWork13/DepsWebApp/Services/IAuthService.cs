using DepsWebApp.Models;

namespace DepsWebApp.Services
{
    /// <summary>
    /// Authorization service abstraction.
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="user">User with login and password.</param>
        /// <returns>
        /// Returns <c>true</c> if user was registered or 
        /// <c>false</c> if user was not registered because login already exists.</returns>
        bool Register(User user);

        /// <summary>
        /// Checks if <paramref name="user"/> is registered.
        /// </summary>
        /// <param name="user">User with login and password.</param>
        /// <returns><c>true</c> if user is registered or <c>false</c> otherwise.</returns>
        bool CheckUser(User user);
    }
}
