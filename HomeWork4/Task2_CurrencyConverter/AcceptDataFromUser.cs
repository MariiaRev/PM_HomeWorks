using System;

namespace Task2_CurrencyConverter
{
    public static class AcceptDataFromUser
    {
        //accept string with fixed length
        public static string AcceptStringWithFixedLength(string message, string tryAgainMessage, int fieldLength)
        {
            Console.WriteLine(message);
            string data = Console.ReadLine().Trim();

            while (data.Length != fieldLength)
            {
                Console.WriteLine(tryAgainMessage);
                data = Console.ReadLine().Trim();
            }

            return data;
        }

        //accept decimal greater than a given threshold
        public static decimal AcceptDecimalGreaterThanThreshold(string message, string tryAgainMessage, decimal threshold)
        {
            decimal amount;

            Console.WriteLine(message);
            string data = Console.ReadLine().Trim();

            while(!decimal.TryParse(data, out amount) || amount <= threshold)
            {
                Console.WriteLine(tryAgainMessage);
                data = Console.ReadLine().Trim();
            }

            return amount;
        }
    }
}
