using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.IO;
using System;
using System.IO;
using System.Net;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace DepsWebApp.Middlewares
{
    /// <summary>
    /// Middleware for enabling custom logging.
    /// </summary>
    public class CustomLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private ILogger<CustomLoggingMiddleware> _logger;
        private readonly RecyclableMemoryStreamManager _recyclableMemoryStreamManager;

        /// <summary>
        /// Constructor with DI.
        /// </summary>
        /// <param name="next">The next middleware in the pipeline.</param>
        /// <param name="logger"><see cref="ILogger"/> where to write logs.</param>
        public CustomLoggingMiddleware(RequestDelegate next, ILogger<CustomLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
            _recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
        }

        /// <summary>
        /// Logging requests with their results.
        /// </summary>
        /// <param name="context"><see cref="HttpContext"/> context.</param>
        /// <returns>Returns no value.</returns>
        public async Task InvokeAsync(HttpContext context)
        {
            // get request body
            var requestBody = await ObtainRequestBody(context.Request);

            // stream substitution
            var originalBodyStream = context.Response.Body;
            await using var memoryStream = _recyclableMemoryStreamManager.GetStream();
            context.Response.Body = memoryStream;

            // invoke controller
            await _next.Invoke(context);

            // get result (response body)
            var responseBody = await ObtainResponseBody(context);

            // replacing the stream back
            await memoryStream.CopyToAsync(originalBodyStream);

            // log request and its result
            var method = context.Request.Method;
            var requestString = context.Request.Path + context.Request.QueryString;
            var responseCode = (HttpStatusCode)context.Response.StatusCode;
            _logger.LogInformation($"{method} request {requestString} with the body '{requestBody}' was executed with the code {responseCode} and the body '{responseBody}'.");
        }

        private static async Task<string> ObtainRequestBody(HttpRequest request)
        {
            if (request.Body == null)
            {
                return string.Empty;
            }

            request.EnableBuffering();
            var encoding = GetEncodingFromContentType(request.ContentType);
            using var reader = new StreamReader(request.Body, encoding, true, 1024, true);
            string bodyStr = await reader.ReadToEndAsync().ConfigureAwait(false);
            request.Body.Seek(0, SeekOrigin.Begin);

            return bodyStr;
        }

        private static async Task<string> ObtainResponseBody(HttpContext context)
        {
            var response = context.Response;
            response.Body.Seek(0, SeekOrigin.Begin);
            var encoding = GetEncodingFromContentType(response.ContentType);
            using var reader = new StreamReader(response.Body, encoding, false, 4096, true);
            var text = await reader.ReadToEndAsync().ConfigureAwait(false);
            response.Body.Seek(0, SeekOrigin.Begin);

            return text;
        }

        private static Encoding GetEncodingFromContentType(string contentTypeStr)
        {
            if (string.IsNullOrEmpty(contentTypeStr))
            {
                return Encoding.UTF8;
            }

            ContentType contentType;
            try
            {
                contentType = new ContentType(contentTypeStr);
            }
            catch (FormatException)
            {
                return Encoding.UTF8;
            }

            if (string.IsNullOrEmpty(contentType.CharSet))
            {
                return Encoding.UTF8;
            }

            return Encoding.GetEncoding(contentType.CharSet, EncoderFallback.ExceptionFallback, DecoderFallback.ExceptionFallback);
        }
    }
}
