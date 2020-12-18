using System;

namespace Library.Exceptions
{
    // if the transaction amount exceeds the established limits in payment methods
    public class LimitExceededException : PaymentServiceException
    {
        public LimitExceededException()
            : base()
        { }

        public LimitExceededException(string message)
            : base(message)
        { }
    }
}
