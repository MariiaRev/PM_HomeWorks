using DepsWebApp.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DepsWebApp.Services
{
    /// <summary>
    /// Authorization service which implements <see cref="IAuthService"/> and stores data in postgres database.
    /// </summary>
    public class AuthPostgresService : IAuthService
    {
        private readonly DepsWebAppContext _dbContext;
        
        /// <summary>
        /// Constructor with DI.
        /// </summary>
        /// <param name="dbContext"></param>
        public AuthPostgresService(DepsWebAppContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc/>
        public async Task<bool> RegisterAsync(User user)
        {
            if (user == null)
            {
                return false;
            }

            var userExists = _dbContext.Users.Where(u => u.Login == user.Login).FirstOrDefault();
            if (userExists == null)
            {
                await _dbContext.AddAsync(user);
                await _dbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        /// <inheritdoc/>
        public bool CheckUser(User user)
        {
            var foundUser = _dbContext.Users
                .FirstOrDefault(u => u.Login == user.Login && u.Password == user.Password);

            if (foundUser == null)
            {
                return false;
            }

            return true;
        }
    }
}
