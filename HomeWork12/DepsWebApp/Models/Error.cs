namespace DepsWebApp.Models
{
    /// <summary>
    /// Error model.
    /// </summary>
    public class Error
    {
        /// <summary>
        /// The error code.
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// The error message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Initializes a new instance of the class <see cref="Error"/> with <paramref name="code"/> and <paramref name="message"/>.
        /// </summary>
        /// <param name="code">The <see cref="Code"/> value.</param>
        /// <param name="message">The <see cref="Message"/> value.</param>
        public Error(int code, string message)
        {
            Code = code;
            Message = message;
        }
    }
}
