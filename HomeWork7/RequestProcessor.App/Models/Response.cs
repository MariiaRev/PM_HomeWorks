using System;
using System.Collections.Generic;
using System.Text;

namespace RequestProcessor.App.Models
{
    /// <inheritdoc/>
    internal class Response : IResponse
    {
        /// <inheritdoc/>
        public bool Handled { get; private set; }

        /// <inheritdoc/>
        public int Code { get; }

        /// <inheritdoc/>
        public string Content { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="code"><see cref="Code"/></param>
        /// <param name="content"><see cref="Content"/></param>
        public Response(int code, string content)
        {
            Handled = true;
            Code = code;
            Content = content;
        }

        /// <summary>
        /// Set <see cref="Handled"/> as false (for timeout error).
        /// </summary>
        public void SetHandledAsFalse()
        {
            Handled = false;
        }
    }
}
