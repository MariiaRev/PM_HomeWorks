using System;
using Library;
using static Task_1._2.Program;
using static Library.DifferentManipulationsWithArrayOfAccounts;

namespace Task_1._3
{
    class Program
    {
        static void Main(string[] args)
        {
            //create and sort accounts
            Account[] sortedAccounts = CreateAndSortAccounts();
            
            //print first x and last x accounts
            PrintFirstAndLastXSortedAccounts(sortedAccounts);


            //try find some Id and try find the Id which 100% is in accounts array
            int accountId1 = 111178;
            int accountId2 = sortedAccounts[sortedAccounts.Length/16].Id;

            Console.WriteLine($"\nSearching of {accountId1} id started...");
            GetAccount(accountId1, sortedAccounts);

            Console.WriteLine($"\nSearching of {accountId2} id started...");
            GetAccount(accountId2, sortedAccounts);



            Console.WriteLine("\n\n\n");
            //Console.Write($"Enter any key:");
            //Console.ReadLine();
            //Console.WriteLine();
        }


        //binary search of Account with specified Id
        public static void GetAccount(int id, Account[] accounts)
        {
            BinarySearch(id, accounts, out int index, out int tries);

            if (index < 0)
                Console.WriteLine($"There is no account {id} in the list");
            else
            {
                Console.WriteLine($"{id} was found at index {index} by {tries} tries");
            }
        }
    }
}
