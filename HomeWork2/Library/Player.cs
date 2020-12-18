using System;
using System.Collections.Generic;

namespace Library
{
    //to work with users
    public class Player
    {
        readonly int Id;
        string Password;
        Account Account { get; }                 //player's financial account

        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }


        const int MIN_ID_VALUE = 100000;
        const int MAX_ID_VALUE = 99999999;

        static HashSet<int> usedIds = new HashSet<int>(MAX_ID_VALUE - MIN_ID_VALUE + 1);

        public Player(string firstName, string lastName, string email, string pswrd, string currency)
        {
            //set id

            Random rand = new Random();

            if (usedIds.Count < MAX_ID_VALUE - MIN_ID_VALUE + 1)
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

            //set user's first and last names, email and password
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = pswrd;

            //create user's account with the currency
            Account = new Account(currency);

        }


        //return whether the player's password matches the suggested password
        public bool IsPasswordValid(string password)
        {
            return password == Password;
        }

        public void Deposit(decimal amount, string Currency)
        {
            Account.Deposit(amount, Currency);
        }

        public void Withdraw(decimal amount, string Currency)
        {
            Account.Withdraw(amount, Currency);
        }

    }
}
