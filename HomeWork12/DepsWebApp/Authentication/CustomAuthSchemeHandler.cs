using DepsWebApp.Models;
using DepsWebApp.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using System;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace DepsWebApp.Authentication
{
    /// <summary>
    /// Custom authentication scheme handler.
    /// </summary>
    public class CustomAuthSchemeHandler : AuthenticationHandler<CustomAuthSchemeOptions>
    {
        private readonly IAuthService _authService;

        /// <summary>
        /// Constructor with DI.
        /// </summary>
        public CustomAuthSchemeHandler(
            IOptionsMonitor<CustomAuthSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IAuthService authService)
            : base(options, logger, encoder, clock)
        {
            _authService = authService;
        }

        /// <summary>
        /// Handling the authentication.
        /// </summary>
        /// <returns>
        /// <see cref="AuthenticateResult.NoResult"/> if authorization header is wrong; 
        /// <see cref="AuthenticateResult.Success(AuthenticationTicket)"/> if authorization header is OK; 
        /// <see cref="AuthenticateResult.Fail(Exception)"/> if an error occured during the authentication.
        /// </returns>
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!TryGetAuthorizationHeaderFromRequest(Request, out var authHeader))
                return AuthenticateResult.NoResult();

            try
            {
                var user = DecodeAuthorizationHeader(authHeader);
                if (_authService.CheckUser(user))
                {
                    return AuthenticateResult.Success(
                        new AuthenticationTicket(
                            new ClaimsPrincipal(new UserAccountIdentity(user.Login)),
                            CustomAuthScheme.Name));
                }

                return AuthenticateResult.NoResult();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error during authentication");
                return AuthenticateResult.Fail(ex);
            }
        }

        private static bool TryGetAuthorizationHeaderFromRequest(HttpRequest request, out string authorizationHeader)
        {
            authorizationHeader = null;
            if (request.Headers.ContainsKey(HeaderNames.Authorization))
            {
                authorizationHeader = request.Headers[HeaderNames.Authorization].FirstOrDefault();
            }
            return !string.IsNullOrEmpty(authorizationHeader);
        }

        private static User DecodeAuthorizationHeader(string authorizationHeader)
        {
            authorizationHeader = authorizationHeader.Replace("Basic ", "");
            byte[] data = Convert.FromBase64String(authorizationHeader);
            var decodedString = Encoding.UTF8.GetString(data);

            var userData = decodedString.Split(new char[] { ':', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            return new User(userData[0], userData[1]);
        }
    }
}
