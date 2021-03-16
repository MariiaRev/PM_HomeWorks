using DepsWebApp.Models;
using System;

namespace DepsWebApp.Mappers
{
    /// <summary>
    /// Mapper for mapping <see cref="Exception"/> model to <see cref="Error"/> model.
    /// </summary>
    public static class ExceptionMapper
    {
        /// <summary>
        /// Maps <paramref name="exception"/> to the <see cref="Error"/> model.
        /// </summary>
        /// <param name="exception">Exception from which to map the error.</param>
        /// <returns><see cref="Error"/> mapped from <paramref name="exception"/></returns>
        public static Error MapExceptionToError(Exception exception)
        {
            var code = MapCodeByExceptionType(exception.GetType());
            return new Error(code, exception.Message);
        }

        private static int MapCodeByExceptionType(Type type)
        {
            return type.Name switch
            {
                "NotImplementedException" => 11,
                "ArgumentNullException" => 12,
                "ArgumentOutOfRangeException" => 13,
                "AuthException" => 21,
                _ => 0,
            };
        }
    }
}
