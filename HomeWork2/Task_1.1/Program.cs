using System;
using System.Collections.Generic;
using Library;

namespace Task_1._1
{
    class Program
    {
        static void Main(string[] args)
        {
            Account accountEUR = new Account("EUR");
            Account accountUSD = new Account("USD");
            Account accountUAH = new Account("UAH");


            accountEUR.Deposit(10, "EUR");
            accountEUR.Withdraw(3, "UAH");

            accountUAH.Deposit(121, "USD");

            try
            {
                accountUSD.Withdraw(5, "USD");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"{ex.Message}\n");
            }
            catch (Exception) { }

            try
            {
                Account accountPLN = new Account("PLN");
            }
            catch (NotSupportedException ex)
            {
                Console.WriteLine($"{ex.Message}\n");
            }
            catch (Exception) { }


            List<Account> accounts = new List<Account>() { accountEUR, accountUAH, accountUSD };

            foreach(Account acc in accounts)
            {
                string currency = acc.GetCurrency();
                Console.WriteLine($"Account with currency {currency} has {acc.GetBalance(currency)} balance.");
            }



            Console.Write($"\n\n\n");
            //Console.Write($"Enter any key:");
            //Console.ReadLine();
            //Console.WriteLine();
        }
    }
}
