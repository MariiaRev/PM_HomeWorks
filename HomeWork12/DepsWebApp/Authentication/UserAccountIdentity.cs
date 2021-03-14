using System.Collections.Generic;
using System.Security.Claims;

namespace DepsWebApp.Authentication
{
    /// <summary>
    /// Identity of user account.
    /// </summary>
    public class UserAccountIdentity : ClaimsIdentity
    {
        /// <summary>
        /// User login.
        /// </summary>
        public string UserLogin { get; set; }

        /// <summary>
        /// Parameterless constructor.
        /// </summary>
        public UserAccountIdentity()
        {
        }

        /// <summary>
        /// Constructor with parameters.
        /// </summary>
        /// <param name="userLogin"></param>
        public UserAccountIdentity(string userLogin)
            : base(CreateClaimsIdentity(userLogin), CustomAuthScheme.Type)
        {
            UserLogin = userLogin;
        }

        private static IEnumerable<Claim> CreateClaimsIdentity(string userLogin)
        {
            return new List<Claim>
            {
                new Claim(DefaultNameClaimType, userLogin),
            };
        }
    }
}
