using System;
using System.Threading.Tasks;
using RequestProcessor.App.Logging;
using RequestProcessor.App.Models;
using RequestProcessor.App.Exceptions;

namespace RequestProcessor.App.Services
{
    /// <summary>
    /// Request performer.
    /// </summary>
    internal class RequestPerformer : IRequestPerformer
    {
        private readonly IRequestHandler _requestHandler;
        private readonly IResponseHandler _responseHandler;
        private readonly ILogger _logger;

        /// <summary>
        /// Constructor with DI.
        /// </summary>
        /// <param name="requestHandler">Request handler implementation.</param>
        /// <param name="responseHandler">Response handler implementation.</param>
        /// <param name="logger">Logger implementation.</param>
        public RequestPerformer(
            IRequestHandler requestHandler, 
            IResponseHandler responseHandler,
            ILogger logger)
        {
            _requestHandler = requestHandler;
            _responseHandler = responseHandler;
            _logger = logger;
        }

        /// <inheritdoc/>
        public async Task<bool> PerformRequestAsync(
            IRequestOptions requestOptions,
            IResponseOptions responseOptions)
        {
            IResponse response;

            //send request
            try
            {
                _logger.Log($"{nameof(RequestPerformer)}: Starting request...");
                response = await _requestHandler.HandleRequestAsync(requestOptions);
                _logger.Log($"{nameof(RequestPerformer)}: Response is get.");
            }
            catch(TimeoutException)
            {
                _logger.Log($"{nameof(RequestPerformer)}: Request timeout exception.");
                response = new Response(408, "Request Timeout");
                ((Response)response).SetHandledAsFalse();
            }
            catch(Exception exception)
            {
                _logger.Log($"{nameof(RequestPerformer)}: {exception.Message}");
                throw new PerformException("Perform exception", exception);
            }
            
            //save response
            try
            {
                _logger.Log($"{nameof(RequestPerformer)}: Saving response to the file.");
                await _responseHandler.HandleResponseAsync(response, requestOptions, responseOptions);
                _logger.Log($"{nameof(RequestPerformer)}: Response is saved.");

                return true;
            }
            catch (Exception exception)
            {
                _logger.Log($"{nameof(RequestPerformer)}: {exception.Message}");
                throw new PerformException("Perform exception", exception);
            }
        }
    }
}
