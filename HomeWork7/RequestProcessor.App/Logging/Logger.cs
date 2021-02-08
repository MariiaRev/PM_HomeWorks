using System;
using System.Diagnostics;

namespace RequestProcessor.App.Logging
{
    internal class Logger : ILogger
    {
        /// <inheritdoc/>
        public void Log(string message)
        {
            if (!string.IsNullOrWhiteSpace(message))
            {
                Debug.WriteLine(message);
            }
        }

        /// <inheritdoc/>
        public void Log(Exception exception, string message)
        {
            if (exception != null && !string.IsNullOrWhiteSpace(exception.Message))
            {
                if (exception.InnerException == null)
                    Debug.WriteLine($"\n\n{message}\nException: {exception.Message}");
                else
                    Debug.WriteLine($"\n\n{message}\nException: {exception.Message}\nInner exception: {exception.InnerException.Message}");
            }
        }
    }
}
