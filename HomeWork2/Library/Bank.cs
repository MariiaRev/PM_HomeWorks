using System;
using Library.Exceptions;
using System.Collections.Generic;

namespace Library
{
    public class Bank : PaymentMethodBase, ISupportDeposit, ISupportWithdrawal
    {
        protected string[] AvailableCards;

        protected decimal? limitForTransactionsSumUAH = null;
        protected decimal? internetLimitUAH = null;

        Dictionary<string, decimal> clientsTransactionsSums = new Dictionary<string, decimal>();        //the sum of transactions of each client

        public void StartDeposit(decimal amount, string currency)
        {
            TransactionProcessing(amount, currency, "withdrawn");
        }

        public void StartWithdrawal(decimal amount, string currency)
        {
            TransactionProcessing(amount, currency, "deposited");
        }

        void TransactionProcessing(decimal amount, string currency, string successfullTransactionOutcome)
        {
            //greetings
            Console.WriteLine($"\n\nWelcome, dear client, to the online bank {Name}");

            //accept login and password
            AcceptLoginAndPasswordFromUser(out string login, out string password);

            //accept client's card type
            string clientCard = AcceptCardTypeFromUser(login);

            //do transaction and check its limits
            DoTransactionWithLimitsChecking(amount, currency, login);

            //if transaction is successful (no exception occured) print message
            Console.WriteLine($"\n\nYou’ve {successfullTransactionOutcome} {amount} {currency} to your {clientCard} card successfully");
        }


        void checkInternetLimit(decimal amount)
        {
            if (internetLimitUAH != null && amount > internetLimitUAH)
                throw new LimitExceededException($"Exceeded the limit of {internetLimitUAH} UAH for Internet transactions.");
        }

        void checkTransactionsSumLimit(decimal amount, string login)
        {
            if (limitForTransactionsSumUAH != null && clientsTransactionsSums[login] + amount > limitForTransactionsSumUAH)
                throw new InsufficientFundsException($"Exceeded the limit of {limitForTransactionsSumUAH} UAH for the sum of all transactions.");
        }


        //emulation of the bank's operation in which an unknown error can occur
        public static void SomeBankWork()
        {
            //bank is doing some work

            const int errorChance = 2;                          //error chance in percentages
            Random rand = new Random();

            int randomNumberFrom1To100 = rand.Next(1, 101);

            if (randomNumberFrom1To100 <= errorChance)          //unknown error occured
                throw new PaymentServiceException();

        }

        void AcceptLoginAndPasswordFromUser(out string login, out string password)
        {
            //accept login
            const int minLoginLength = 3;
            ValidateUserStringData("login", minLoginLength, out login);

            //accept password
            const int minPasswordLength = 4;
            ValidateUserStringData("password", minPasswordLength, out password);
        }

        void ValidateUserStringData(string data, int minValue, out string dataValue)
        {
            Console.WriteLine($"\n\nPlease, enter your {data}: ");
            dataValue = Console.ReadLine().Trim();

            while (dataValue == "" || dataValue.Length < minValue)
            {
                Console.WriteLine($"\n\nThe {data} must be minimum {minValue} symbols length!");
                Console.Write("Try again, please: ");
                dataValue = Console.ReadLine();
                Console.WriteLine();
            }
        }

        void PrintAvailableCards()
        {
            Console.WriteLine("\nAvailableCards: ");

            for (int i = 0; i < AvailableCards.Length; i++)
                Console.WriteLine($"{i + 1}. {AvailableCards[i]}");
            Console.WriteLine();
        }

        string AcceptCardTypeFromUser(string login)
        {
            //choose card
            Console.WriteLine($"\n\nHello Mr/Ms {login}. Pick a card to proceed the transaction. ");

            PrintAvailableCards();

            bool success = int.TryParse(Console.ReadLine(), out int chosenCard);

            while (!success || chosenCard < 1 || chosenCard > AvailableCards.Length)
            {
                Console.WriteLine("\n\nWrong number! Available cards: ");

                for (int i = 0; i < AvailableCards.Length; i++)
                    Console.WriteLine($"{i + 1}. {AvailableCards[i]}");
                Console.WriteLine();

                success = int.TryParse(Console.ReadLine(), out chosenCard);
            }

            return AvailableCards[chosenCard - 1];
        }

        void DoTransactionWithLimitsChecking(decimal amount, string currency, string login)
        {
            SomeBankWork();

            decimal amountConvertedToUAH = ConvertMoney.ConvertAmount(amount, currency, "UAH", ExchangeRate.GetExchangeRate());     //convert amount to UAH for limits check

            checkInternetLimit(amountConvertedToUAH);                            //checking if the amount exceeds the Internet limit

            clientsTransactionsSums.TryAdd(login, 0);
            checkTransactionsSumLimit(amountConvertedToUAH, login);              //checking if the client with the "login" has exceeded the limit on the transactions sum
            clientsTransactionsSums[login] += amount;
        }
    }
}
