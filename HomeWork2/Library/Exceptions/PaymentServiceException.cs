using System;

namespace Library.Exceptions
{
    // if PaymentService has problems in its work
    public class PaymentServiceException : Exception
    {
        public PaymentServiceException()
            : base()
        { }


        //exception with a specified error message
        public PaymentServiceException(string message)
            : base(message)
        { }


        //exception with a specified error message and name of method, which caused the exception
        public PaymentServiceException(string message, string methodName)
            : base(message)
        {
            MethodName = methodName;
        }

        public string MethodName { get; }

    }
}
