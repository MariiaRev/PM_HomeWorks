using System;
using Library;
using static Library.DifferentManipulationsWithArrayOfAccounts;

namespace Task_1._4
{
    class Program
    {
        static void Main(string[] args)
        {
            const int ACCOUNTS_NUMBER = 1000000;
            const int ACCOUNTS_NUMBER_TO_SHOW = 10;


            //create accounts
            Account[] accounts = CreateArrayOfAccounts(ACCOUNTS_NUMBER, "UAH");

            Console.WriteLine($"Created {ACCOUNTS_NUMBER} accounts.");

            //sort accounts
            Account[] sortedAccounts = GetSortedAccountsByQuickSort(accounts);

            Console.WriteLine($"Sorted {ACCOUNTS_NUMBER} accounts.");


            //print first x and last x elements

            Console.WriteLine($"\n\nFirst {ACCOUNTS_NUMBER_TO_SHOW} accounts are:");

            for (int i = 0; i < ACCOUNTS_NUMBER_TO_SHOW; i++)
                Console.WriteLine(sortedAccounts[i].Id);


            Console.WriteLine($"\n\nLast {ACCOUNTS_NUMBER_TO_SHOW} accounts are:");

            int from = sortedAccounts.Length - ACCOUNTS_NUMBER_TO_SHOW;
            for (int i = from; i < sortedAccounts.Length; i++)
                Console.WriteLine(sortedAccounts[i].Id);




            Console.WriteLine("\n\n\n");
            //Console.Write($"Enter any key:");
            //Console.ReadLine();
            //Console.WriteLine();

        }

        //get accounts sorted by quick sort
        static Account[] GetSortedAccountsByQuickSort(Account[] accounts)
        {
            Account[] sortedAccounts = new Account[accounts.Length];
            Array.Copy(accounts, sortedAccounts, accounts.Length);              //to change only sortedAccounts but not accounts

            QuickSort(sortedAccounts, 0, sortedAccounts.Length-1);

            return sortedAccounts;
        }

    }
}
