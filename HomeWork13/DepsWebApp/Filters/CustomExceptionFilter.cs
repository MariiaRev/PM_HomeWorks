using DepsWebApp.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DepsWebApp.Filters
{
    /// <summary>
    /// Custom exception filter.
    /// </summary>
    public class CustomExceptionFilter : IExceptionFilter
    {
        /// <summary>
        /// Executes when an exception occurs.
        /// </summary>
        /// <param name="context">The context of the exception.</param>
        public void OnException(ExceptionContext context)
        {
            var error = ExceptionMapper.MapExceptionToError(context.Exception);
            context.Result = new JsonResult(error);
        }
    }
}
