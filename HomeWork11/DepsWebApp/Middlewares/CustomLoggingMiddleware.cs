using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DepsWebApp.Middlewares
{
    /// <summary>
    /// Middleware for enabling custom logging.
    /// </summary>
    public class CustomLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="next">The next middleware in the pipeline.</param>
        public CustomLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Invoking logging.
        /// </summary>
        /// <param name="context"><see cref="HttpContext"/> context.</param>
        /// <param name="logger"><see cref="ILogger"/> where to write logs.</param>
        /// <returns>Returns no value (task).</returns>
        public Task Invoke(HttpContext context, ILogger logger)
        {
            var responseContent = context.Response.Body.ToString();
            logger.LogInformation($"Request {responseContent} was executed with result {"some result"}");

            //_next.Invoke(context);        //need here?
            return Task.CompletedTask;
        }

        //private static async Task<string> ObtainRequestBody(HttpRequest request)
        //{
        //    if (request.Body == null)
        //        return string.Empty;
            
        //    request.EnableBuffering();
        //    var encoding = GetEncodingFromContentType(request.ContentType);
        //    string bodyStr;

        //    using (var reader = new StreamReader(request.Body, encoding, true, 1024, true))
        //    {
        //        bodyStr = await reader.ReadToEndAsync().ConfigureAwait(false);
        //    }

        //    request.Body.Seek(0, SeekOrigin.Begin);
            
        //    return bodyStr;
        //}

        //private static async Task<string> ObtainResponseBody(HttpContext context)
        //{
        //    var response = context.Response;
        //    response.Body.Seek(0, SeekOrigin.Begin);
        //    var encoding = GetEncodingFromContentType(response.ContentType);
            
        //    using (var reader = new StreamReader(response.Body, encoding, 
        //        detectEncodingFromByteOrderMarks: false, bufferSize: 4096, leaveOpen: true))
        //    {
        //        var text = await reader.ReadToEndAsync().ConfigureAwait(false);
        //        response.Body.Seek(0, SeekOrigin.Begin);
                
        //        return text;
        //    }
        //}
    }
}
