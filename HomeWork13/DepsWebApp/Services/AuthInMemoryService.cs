using DepsWebApp.Models;
using System.Collections.Concurrent;
using System.Linq;

namespace DepsWebApp.Services
{
    /// <summary>
    /// In-memory authorization service which implements <see cref="IAuthService"/>.
    /// </summary>
    public class AuthInMemoryService : IAuthService
    {
        private readonly ConcurrentDictionary<string, User> _users = new ConcurrentDictionary<string, User>();

        /// <inheritdoc/>
        public bool Register(User user)
        {
            if (_users.TryAdd(user.Login, user))
            {
                return true;
            }

            return false;
        }

        /// <inheritdoc/>
        public bool CheckUser(User user)
        {
            var registeredUser = _users.Values.FirstOrDefault(u => u.Login == user.Login && u.Password == user.Password);
            
            if (registeredUser == null)
            {
                return false;
            }

            return true;
        }
    }
}
