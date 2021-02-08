using System;

namespace RequestProcessor.App.Models
{
    internal class RequestOptions : IRequestOptions, IResponseOptions
    {
        /// <inheritdoc/>
        public string Name { get; }

        /// <inheritdoc/>
        public string Address { get; }

        /// <inheritdoc/>
        public RequestMethod Method { get; }

        /// <inheritdoc/>
        public string ContentType { get; }

        /// <inheritdoc/>
        public string Body { get; }

        /// <inheritdoc/>
        public string Path { get; }

        /// <inheritdoc/>
        bool IRequestOptions.IsValid => ValidateRequestOptions();

        /// <inheritdoc/>
        bool IResponseOptions.IsValid => ValidateResponseOptions();

        public RequestOptions(string name, string address, RequestMethod method, string contentType, string body, string path)
        {
            Name = name;
            Address = address;
            Method = method;
            ContentType = contentType;
            Body = body;
            Path = path;
        }

        // validation methods

        bool ValidateRequestOptions()
        {
            return ValidateAdress() &&
                   ValidateMethod() &&
                   ValidateBodyAndContentType();
        }

        bool ValidateResponseOptions()
        {
            return !string.IsNullOrWhiteSpace(Path);
        }

        // additional request validation methods

        bool ValidateAdress()
        {
            return Uri.TryCreate(Address, UriKind.Absolute, out _);
        }

        bool ValidateMethod()
        {
            return (Method == RequestMethod.Delete || Method == RequestMethod.Get || Method == RequestMethod.Patch ||
                    Method == RequestMethod.Post || Method == RequestMethod.Put);
        }

        bool ValidateBodyAndContentType()
        {
            return string.IsNullOrWhiteSpace(Body) || (!string.IsNullOrWhiteSpace(ContentType));
        }
    }
}
