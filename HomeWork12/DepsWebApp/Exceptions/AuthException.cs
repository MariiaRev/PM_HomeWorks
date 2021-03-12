using System;

namespace DepsWebApp.Exceptions
{
    /// <summary>
    /// The exception that is thrown when the authorization cannot be executed. 
    /// </summary>
    public class AuthException: Exception
    {
        /// <summary>
        /// The name of the parameter that causes this exception.
        /// </summary>
        public string ParamName { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthException"/> class.
        /// </summary>
        public AuthException() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthException"/> class with the <paramref name="message"/>.
        /// </summary>
        /// <param name="message">Specified error message.</param>
        public AuthException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthException"/> class 
        /// with the <paramref name="paramName"/> that causes this exception and the <paramref name="message"/>.
        /// </summary>
        /// <param name="paramName">The parameter name that causes this exception.</param>
        /// <param name="message"></param>
        public AuthException(string paramName, string message)
            : base(message)
        {
            ParamName = paramName;
        }
    }
}
