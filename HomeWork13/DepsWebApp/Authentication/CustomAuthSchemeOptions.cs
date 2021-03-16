using Microsoft.AspNetCore.Authentication;

namespace DepsWebApp.Authentication
{
    /// <summary>
    /// Custom authentication scheme options.
    /// </summary>
    public class CustomAuthSchemeOptions : AuthenticationSchemeOptions
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public CustomAuthSchemeOptions()
        {
            ClaimsIssuer = CustomAuthScheme.Issuer;
        }
    }
}
