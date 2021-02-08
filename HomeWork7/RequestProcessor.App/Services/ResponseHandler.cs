using System;
using System.IO;
using System.Threading.Tasks;
using RequestProcessor.App.Models;

namespace RequestProcessor.App.Services
{
    /// <inheritdoc/>
    internal class ResponseHandler : IResponseHandler
    {
        /// <inheritdoc/>
        public Task HandleResponseAsync(IResponse response, IRequestOptions requestOptions, IResponseOptions responseOptions)
        {
            if (response == null || requestOptions == null || responseOptions == null)
                throw new ArgumentNullException("One of required parameters are missing.");

            var handled = response.Handled ? "handled" : "not handled";
            var method = requestOptions.Method.ToString().ToUpper();
            var message = $"The request \"{requestOptions.Name}\" with {method} method to the {requestOptions.Address} worked with the code {response.Code} and was {handled}.\nContent:\n{response.Content}";

            return File.WriteAllTextAsync(responseOptions.Path, message);
        }
    }
}
