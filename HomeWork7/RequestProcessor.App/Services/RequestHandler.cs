using RequestProcessor.App.Models;
using RequestProcessor.App.Mappings;
using System;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;

namespace RequestProcessor.App.Services
{
    /// <inheritdoc/>
    internal class RequestHandler : IRequestHandler
    {
        readonly HttpClient _client;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="client">Http client.</param>
        public RequestHandler(HttpClient client)
        {
            _client = client;
        }

        /// <inheritdoc/>
        public async Task<IResponse> HandleRequestAsync(IRequestOptions requestOptions)
        {
            if (requestOptions == null)
                throw new ArgumentNullException(nameof(requestOptions), "Request options are missing.");

            if (!requestOptions.IsValid)
                throw new ArgumentOutOfRangeException(nameof(requestOptions), "Request options are not valid.");

            //set method
            var method = RequestMethodMappings.MapRequestMethod(requestOptions.Method);
            
            //set request
            var request = new HttpRequestMessage(method, requestOptions.Address);
            if (!string.IsNullOrWhiteSpace(requestOptions.Body))
            {
                request.Content = new StringContent(requestOptions.Body, Encoding.Default, requestOptions.ContentType);
            }

            var result = await _client.SendAsync(request);
            result.Content.Headers.ContentType.CharSet = "utf-8";

            return new Response((int)result.StatusCode, await result.Content.ReadAsStringAsync());
        }
    }
}
