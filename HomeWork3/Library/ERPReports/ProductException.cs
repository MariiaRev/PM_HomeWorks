using System;

namespace Library.ERPReports
{
    public class ProductException : Exception
    {
        public ProductException(string message)
            : base(message)
        { }
    }
}
