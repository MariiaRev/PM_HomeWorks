using System;
using System.Collections.Generic;
using System.Text;

namespace Library
{
    public static class DifferentManipulationsWithArrayOfAccounts
    {

        //create array of accountsNumber accounts with the currency
        public static Account[] CreateArrayOfAccounts (int accountsNumber, string currency)
        {
            Account[] accounts = new Account[accountsNumber];

            for (int i = 0; i < accountsNumber; i++)
                accounts[i] = new Account(currency);

            return accounts;
        }


        //sort array of accounts by bubble sort
        public static void BubbleSort(Account[] accounts)
        {
            for (int i = 0; i < accounts.Length; i++)
            {
                for (int j = 0; j < accounts.Length - 1; j++)
                {
                    if (accounts[j].Id > accounts[j + 1].Id)
                    {
                        Account temp = accounts[j];
                        accounts[j] = accounts[j + 1];
                        accounts[j + 1] = temp;
                    }
                }
            }
        }

        //search the account with specified Id by binary search
        public static void BinarySearch(int id, Account[] accounts, out int index, out int tries)
        {
            tries = 0;
            index = -1;

            int left = 0;
            int right = accounts.Length - 1;
            int middle;

            while (true)
            {
                tries++;

                middle = (left + right) / 2;

                if (id == accounts[middle].Id)                           //Id is found
                {
                    index = middle;
                    break;
                }

                if (left == middle || right == middle)                  //Id is not found
                {
                    break;
                }

                if (id > accounts[middle].Id)
                {
                    left = middle + 1;
                }
                else
                {
                    right = middle - 1;
                }
            }
        }


        //sort array of accounts by quick sort
        public static void QuickSort(Account[] accounts, int left, int right)
        {
            if (left < right)
            {
                int pivot = right;
                int i = left - 1;

                for (int j = left; j < right; j++)
                {
                    if (accounts[j].Id < accounts[pivot].Id)
                    {
                        i++;

                        if (i != j)
                        {
                            SwapAccounts(ref accounts[i], ref accounts[j]);
                        }
                    }
                }

                if (i + 1 != pivot)
                {
                    SwapAccounts(ref accounts[i + 1], ref accounts[pivot]);
                }


                QuickSort(accounts, left, i);
                QuickSort(accounts, i + 2, right);
            }
        }

        static void SwapAccounts(ref Account acc1, ref Account acc2)
        {
            Account temp = acc1;
            acc1 = acc2;
            acc2 = temp;
        }

    }
}