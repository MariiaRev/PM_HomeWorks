using System;
using System.Net.Http;
using RequestProcessor.App.Models;

namespace RequestProcessor.App.Mappings
{
    internal static class RequestMethodMappings
    {
        public static HttpMethod MapRequestMethod(RequestMethod requestMethod)
        {
            return requestMethod switch
            {
                RequestMethod.Get => HttpMethod.Get,
                RequestMethod.Post => HttpMethod.Post,
                RequestMethod.Put => HttpMethod.Put,
                RequestMethod.Patch => HttpMethod.Patch,
                RequestMethod.Delete => HttpMethod.Delete,
                _ => throw new ArgumentOutOfRangeException(nameof(requestMethod), "Undefined request method."),
            };
        }
    }
}
