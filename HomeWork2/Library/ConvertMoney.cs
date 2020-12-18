using System;
using System.Collections.Generic;
using System.Text;

namespace Library
{
    public static class ConvertMoney
    {
        public static decimal ConvertAmount(decimal amount, string currencyFrom, string currencyTo, Dictionary<string, decimal> exchangeRate)
        {
            if (currencyFrom == currencyTo)
                return amount;

            decimal convertedAmount = 0m;

            switch (currencyFrom)
            {
                case "UAH":                                                          //convert UAN
                    {
                        if (currencyTo == "USD")                                       //to USD
                            convertedAmount = amount / exchangeRate["USD-UAH"];

                        else if (currencyTo == "EUR")                                  //to EUR
                            convertedAmount = amount / exchangeRate["EUR-UAH"];

                    }; break;

                case "USD":                                                         //convert USD
                    {
                        if (currencyTo == "EUR")                                      //to EUR
                            convertedAmount = amount / exchangeRate["EUR-USD"];

                        else if (currencyTo == "UAH")                                 //to UAH
                            convertedAmount = amount * exchangeRate["USD-UAH"];

                    }; break;

                case "EUR":                                                         //convert EUR
                    {
                        if (currencyTo == "USD")                                      //to USD
                            convertedAmount = amount * exchangeRate["EUR-USD"];

                        else if (currencyTo == "UAH")                                 //to UAH
                            convertedAmount = amount * exchangeRate["EUR-UAH"];

                    }; break;

                default: convertedAmount = 0m; break;
            }

            return convertedAmount;
        }

    }
}
