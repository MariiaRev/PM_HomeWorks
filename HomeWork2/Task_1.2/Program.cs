using System;
using Library;
using static Library.DifferentManipulationsWithArrayOfAccounts;

namespace Task_1._2
{
    public class Program
    {
        static void Main(string[] args)
        {
            Account[] accounts = CreateAndSortAccounts();
            PrintFirstAndLastXSortedAccounts(accounts);



            Console.WriteLine("\n\n\n");
            //Console.Write($"Enter any key:");
            //Console.ReadLine();
            //Console.WriteLine();
        }

        //sort accounts by bubble sort
        static Account[] GetSortedAccounts(Account[] accounts)
        {
            Account[] sortedAccounts = new Account[accounts.Length];
            Array.Copy(accounts, sortedAccounts, accounts.Length);              //to change only sortedAccounts but not accounts

            BubbleSort(sortedAccounts);

            return sortedAccounts;
        }

        public static Account[] CreateAndSortAccounts()
        {
            const int ACCOUNTS_NUMBER = 1000000;

            //create accounts
            Account[] accounts = CreateArrayOfAccounts(ACCOUNTS_NUMBER, "UAH");

            Console.WriteLine($"Created {ACCOUNTS_NUMBER} accounts.");

            //sort accounts
            Account[] sortedAccounts = GetSortedAccounts(accounts);

            Console.WriteLine($"Sorted {ACCOUNTS_NUMBER} accounts.");

            return sortedAccounts;
        }

        public static void PrintFirstAndLastXSortedAccounts(Account[] sortedAccounts)
        {
            const int ACCOUNTS_NUMBER_TO_SHOW = 10;

            //print first x and last x accounts

            Console.WriteLine($"\n\nFirst {ACCOUNTS_NUMBER_TO_SHOW} accounts are:");

            for (int i = 0; i < ACCOUNTS_NUMBER_TO_SHOW; i++)
                Console.WriteLine(sortedAccounts[i].Id);


            Console.WriteLine($"\n\nLast {ACCOUNTS_NUMBER_TO_SHOW} accounts are:");

            int from = sortedAccounts.Length - ACCOUNTS_NUMBER_TO_SHOW;
            for (int i = from; i < sortedAccounts.Length; i++)
                Console.WriteLine(sortedAccounts[i].Id);
        }
    }
}
