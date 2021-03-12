using Microsoft.AspNetCore.Builder;
using DepsWebApp.Middlewares;

namespace DepsWebApp.Extensions
{
    /// <summary>
    /// Extensions for custom logging.
    /// </summary>
    public static class CustomLoggingExtensions
    {
        /// <summary>
        /// Connects the <see cref="CustomLoggingMiddleware"/> middleware to the specified
        /// <see cref="IApplicationBuilder"/> which enables custom logging.
        /// </summary>
        /// <param name="builder">The <see cref="IApplicationBuilder"/> to add the middleware to.</param>
        /// <returns>Returns a reference to this instance after the operation has completed.</returns>
        public static IApplicationBuilder UseCustomLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomLoggingMiddleware>();
        }
    }
}
