using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Task2_CurrencyConverter
{
    class Program
    {
        static async Task Main()
        {
            //print greetings
            Console.WriteLine($"{"",22}This is Homework 4 Task 2.\n{"",27}Currency converter");
            Console.WriteLine($"\n{"",21}### Made by Mariia Revenko ###\n\n");

            const int CurrencyCodeLength = 3;
            const string CachePath = "cache.json";
            const string Uah = "UAH";
            string cache;                                   //cache from file

            while (true)
            {
                //get input currency
                var inputCurrency = AcceptDataFromUser.AcceptStringWithFixedLength("\n\nEnter input currency, please:",
                    $"\nInvalid input. The currency must be exactly {CurrencyCodeLength} characters long. Try again: ",
                    CurrencyCodeLength);

                //get output currency
                var outputCurrency = AcceptDataFromUser.AcceptStringWithFixedLength("\n\nEnter output currency, please:",
                    $"\nInvalid input. The currency must be exactly {CurrencyCodeLength} characters long. Try again: ",
                    CurrencyCodeLength);



                //get amount
                var amount = AcceptDataFromUser.AcceptDecimalGreaterThanThreshold("\n\nEnter the amount, please:",
                    "\nInvalid input. The amount must be set and be greater than 0. Try again:",
                    0);

                await TryUpdateLocalCacheAsync(CachePath);

                //try read cache
                try
                {
                    cache = File.ReadAllText(CachePath);
                }
                catch (FileNotFoundException)
                {
                    //file was not found
                    Console.WriteLine($"\n\nFile {CachePath} is not found.\n\n\n");
                    return;
                }


                //file was found

                //deserialize cache into list
                var rates = JsonConvert.DeserializeObject<List<Cache>>(cache,
                    new IsoDateTimeConverter { DateTimeFormat = "dd.MM.yyyy" });

                //convert amount and print result
                if (string.Equals(inputCurrency, Uah, StringComparison.OrdinalIgnoreCase))
                    DoConvertFromUah(outputCurrency, Uah, rates, amount);

                else if (string.Equals(outputCurrency, Uah, StringComparison.OrdinalIgnoreCase))
                    DoConvertToUah(inputCurrency, Uah, rates, amount);

                else
                    DoDoubleConvert(inputCurrency, outputCurrency, Uah, rates, amount);

                //ask whether or not to continue the program
                Console.WriteLine("Continue? 0 - no, else - yes");
                var choice = Console.ReadLine().Trim();

                if (choice == "0")
                    return;
            }
        }

        static async Task TryUpdateLocalCacheAsync(string cachePath)
        {
            var uri = new Uri("https://bank.gov.ua");                   //uri of NBU
            var httpClient = new HttpClient
            {
                BaseAddress = uri                                       //set uri as base adress at http client
            };                                      //creating http client

            try
            {
                var response = await httpClient.GetAsync("/NBUStatService/v1/statdirectory/exchange?json");

                response.EnsureSuccessStatusCode();
                
                //if status code is 2xx

                //get response content
                var body = await response.Content.ReadAsStringAsync();

                //update exchange rates
                File.WriteAllText(cachePath, body);                         //writing excange rates in json format to the local cache file
            }
            catch(HttpRequestException)
            {
                Console.WriteLine("\n\nFailed to update exchange rates.");
            }
            catch(TaskCanceledException)
            {
                Console.WriteLine("\n\nFailed to update exchange rates.");
            }
        }

        static Cache CurrencyInList(List<Cache> rates, string currency)
        {
            return rates.Find(c => c.CurrencyCode.ToLower() == currency.ToLower());
        }

        static decimal ConvertToUah(decimal amount, decimal rate)
        {
            return amount * rate;
        }

        static decimal ConvertFromUah(decimal amount, decimal rate)
        {
            return amount / rate;
        }

        static void PrintConvertedAmount(Cache inputCache, Cache outputCache, string uah, decimal amount, decimal convertedAmount)
        {
            Console.WriteLine("\n\n\n\tCONVERSION RESULTS\n");

            var date = (DateTime)(inputCache?.ExchangeDate ?? outputCache?.ExchangeDate);
            Console.WriteLine($"Exchange date: {date:dd.MM.yyyy}");

            if (inputCache != null)
                Console.WriteLine($"Exchange rate: 1 {inputCache.CurrencyCode} is {inputCache.Rate} {uah}");

            if (outputCache != null)
                Console.WriteLine($"Exchange rate: 1 {uah} is {Math.Round(1 / outputCache.Rate, 4)} {outputCache.CurrencyCode}");

            Console.WriteLine($"\nConverted amount is {Math.Round(convertedAmount, 2)}\n\n\n");
        }

        static void DoConvertToUah(string inputCurrency, string Uah, List<Cache> rates, decimal amount)
        {
            var inputCache = FindCacheForCurrency(inputCurrency, rates);
;
            if (inputCache != null)
            {
                var convertedAmount = ConvertToUah(amount, inputCache.Rate);
                //print result
                PrintConvertedAmount(inputCache, null, Uah, amount, convertedAmount);
            }
        }

        static void DoConvertFromUah(string outputCurrency, string Uah, List<Cache> rates, decimal amount)
        {
            var outputCache = FindCacheForCurrency(outputCurrency, rates);

            if (outputCache != null)
            {
                var convertedAmount = ConvertFromUah(amount, outputCache.Rate);
                //print result
                PrintConvertedAmount(null, outputCache, Uah, amount, convertedAmount);
            }
        }

        static void DoDoubleConvert(string inputCurrency, string outputCurrency, string uah, List<Cache> rates, decimal amount)
        {
            var inputCache = FindCacheForCurrency(inputCurrency, rates);

            if (inputCache != null)
            {
                var outputCache = FindCacheForCurrency(outputCurrency, rates);

                if (outputCache != null)
                {
                    var convertedToUah = ConvertToUah(amount, inputCache.Rate);
                    var convertedAmount = ConvertFromUah(convertedToUah, outputCache.Rate);
                    //print result
                    PrintConvertedAmount(inputCache, outputCache, uah, amount, convertedAmount);
                }
            }
        }

        static Cache FindCacheForCurrency(string currency, List<Cache> rates)
        {
            var cache = CurrencyInList(rates, currency);

            if (cache == null)
            {
                Console.WriteLine($"\n\nCurrency {currency} was not found.\n\n\n");
            }

            return cache;
        }
    }
}
