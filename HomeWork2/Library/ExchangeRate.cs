using System;
using System.Collections.Generic;
using System.Text;

namespace Library
{
    public static class ExchangeRate
    {
        static Dictionary<string, decimal> exchangeRate = new Dictionary<string, decimal>(3)
        {
            { "USD-UAH", 28.36m },
            { "EUR-UAH", 33.63m },
            { "EUR-USD", 1.19m },
        };

        public static Dictionary<string, decimal> GetExchangeRate()
        {
            return exchangeRate;
        }
    }
}
