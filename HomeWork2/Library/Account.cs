using System;
using System.Collections.Generic;
using System.Linq;

namespace Library
{
    //for work with financial balance
    public class Account
    {
        const int MIN_ID_VALUE = 100000;
        const int MAX_ID_VALUE = 99999999;

        readonly Dictionary<string, decimal> exchangeRate = ExchangeRate.GetExchangeRate();

        static string[] supportedCurrencies = new[] { "UAH", "USD", "EUR" };
        static HashSet<int> usedIds = new HashSet<int>(MAX_ID_VALUE - MIN_ID_VALUE + 1);


        public int Id { get; }
        readonly string Currency;
        decimal Amount;

        public Account(string currency)
        {
            // SET ID

            Random rand = new Random();

            if(usedIds.Count < MAX_ID_VALUE - MIN_ID_VALUE + 1)
            {
                int possibleId;
                while (true)
                {
                    possibleId = rand.Next(MIN_ID_VALUE, MAX_ID_VALUE + 1);
                    bool idIsAvailable = usedIds.Add(possibleId);
                    if (idIsAvailable)
                        break;
                }

                Id = possibleId;

            }
            else
                throw new ArgumentOutOfRangeException(nameof(Id), "Available ids have run out.");


            //SET CURRENCY
            if (supportedCurrencies.Contains(currency))
                Currency = currency;
            else
                throw new NotSupportedException("This currency is not supported.");


            //SET INITIAL AMOUNT
            Amount = 0;
        }


        //top up the account balance for the amount in the currency
        public void Deposit(decimal amount, string currency)
        {
            if (amount >= 0)
            {
                if (supportedCurrencies.Contains(currency))
                {
                    //in the same currency as the account
                    if (currency == Currency)
                    {
                        Amount += amount;
                        return;
                    }

                    //in a different currency
                    Amount += ConvertMoney.ConvertAmount(amount, currency, Currency, exchangeRate);
                    return;
                }
                else
                    throw new NotSupportedException("This currency is not supported.");

            }

            throw new ArgumentOutOfRangeException("Amount cannot be negative.", nameof(amount));
        }

        //withdraw the amount in the currency from the account
        public void Withdraw(decimal amount, string currency)
        {
            //if currency is different than the account's one - convert
            decimal convertedAmount;
            
            if (currency == Currency)                                      //currency is the same with the account
            {
                convertedAmount = amount;
            }
            else                                                                //currency is different
            {
                convertedAmount = ConvertMoney.ConvertAmount(amount, currency, Currency, exchangeRate);
            }


            //if amount to withdraw is more than the account amount
            if (convertedAmount > Amount)
                throw new InvalidOperationException("Not enough money in the account.");

            if (convertedAmount >= 0)
            {
                if (supportedCurrencies.Contains(currency))
                {
                    Amount -= convertedAmount;
                    return;
                }
                else
                    throw new NotSupportedException("This currency is not supported.");

            }

            throw new ArgumentOutOfRangeException("Amount cannot be negative.", nameof(amount));
        }

        //balance in the requested currency according to the exchange rate
        public decimal GetBalance(string currency)
        {
            if (supportedCurrencies.Contains(currency))
            {
                if (currency == Currency)
                    return Amount;

                return ConvertMoney.ConvertAmount(Amount, Currency, currency, exchangeRate);
            }
            else
                throw new NotSupportedException("This currency is not supported.");
        }

        public string GetCurrency()
        {
            return Currency;
        }

        public static string[] GetSupportedCurrencies()
        {
            return supportedCurrencies;
        }

    }
}
