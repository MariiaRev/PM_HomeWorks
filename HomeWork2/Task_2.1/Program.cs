using System;
using System.Threading;             //for BetAllMoney()
using Library;

namespace Task_2._1
{
    class Program
    {
        static void Main(string[] args)
        {
            BetService betService = new BetService();


            //point 1

            Console.WriteLine("------------------------- POINT 1 -------------------------\n");

            //ten odds changes
            for (int i = 1; i <= 10; i++)
            {
                betService.GetOdds();
            }

            decimal betResult = betService.Bet(100);
            Console.WriteLine($"\nI’ve bet 100 USD with the odd {betService.Odd} and I’ve earned {betResult} USD\n\n\n");


            //point 2

            Console.WriteLine("------------------------- POINT 2 -------------------------\n");

            while (betService.Odd < 12)
                betService.GetOdds();

            decimal[] betResults = new decimal[3];

            for (int i = 0; i < betResults.Length; i++)
            {
                betResults[i] = betService.Bet(100);
                Console.WriteLine($"\nI’ve bet 100 USD with the odd {betService.Odd} and I’ve earned {betResults[i]} USD");

                do
                {
                    betService.GetOdds();
                } while (betService.Odd < 12);
            }

            Console.WriteLine("\n\n");

            //point 3

            Console.WriteLine("------------------------- POINT 3 -------------------------\n");

            BetAllMoney(10000);

        }

        static void BetAllMoney(decimal amount)
        {
            Account myAccount = new Account("USD");
            myAccount.Deposit(amount, "USD");
            decimal myBalance = myAccount.GetBalance("USD");

            BetService betService = new BetService();
            decimal result = 0;
            float odd;

            Random rand = new Random();
            decimal sum;

            while (myBalance > 0 && result < 150000)
            {
                do
                {
                    odd = betService.GetOdds();
                } while (odd > 2);

                if (myBalance > 50)
                    sum = rand.Next(1, (int)myBalance + 1);
                else
                    sum = myBalance;


                myAccount.Withdraw(sum, "USD");
                result = Math.Round(betService.Bet(sum), 2);

                myAccount.Deposit(result, "USD");
                myBalance = myAccount.GetBalance("USD");


                Console.WriteLine($"\nI’ve bet {sum} USD with the odd {odd} and I’ve earned {result} USD");
                Console.WriteLine($"My balance is {Math.Round(myBalance, 2)}  USD");
                //Thread.Sleep(5000);               //to display the bet results in slow motion
            }

            Console.WriteLine($"\n\n\nGame over. My balance is {Math.Round(myAccount.GetBalance("USD"), 2)}");
        }
    }
}
