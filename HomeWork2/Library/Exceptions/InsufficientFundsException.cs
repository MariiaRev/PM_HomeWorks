using System;

namespace Library.Exceptions
{
    // if the transaction amount exceeds the account balance of the payment method.
    public class InsufficientFundsException : PaymentServiceException
    {
        public InsufficientFundsException()
            : base()
        { }

        public InsufficientFundsException(string message)
            : base(message)
        { }

        public InsufficientFundsException(string message, string methodName)
           : base(message, methodName)
        { }

    }
}
