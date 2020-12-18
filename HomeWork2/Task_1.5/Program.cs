using System;
using Library;

namespace Task_1._5
{
    class Program
    {
        static void Main(string[] args)
        {
            //checking the Player class
            Player player1 = new Player("James Peter","White","james.white@gmail.com","!@me$2020","USD");
            bool success;

            string[] passwordTries = new[] { "!@me$2020", "jame$2020" };

            foreach(string pswrd in passwordTries)
            {
                success = player1.IsPasswordValid(pswrd);
                Console.WriteLine($"Login with login {player1.Email} and password {pswrd} is {success}");
            }


            player1.Deposit(100, "USD");
            player1.Withdraw(50, "EUR");

            try
            {
                player1.Withdraw(1000, "USD");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"\n\n{ex.Message}");
            }
            catch(Exception) { }

            try
            {
                Player player2 = new Player("John", "Johnson", "johnjohnson@gmail.com", "realPasword", "PLN");
            }
            catch (NotSupportedException ex)
            {
                Console.WriteLine($"\n\n{ex.Message}");
            }
            catch (Exception) { }




            Console.WriteLine("\n\n\n");
            //Console.Write($"Enter any key:");
            //Console.ReadLine();
            //Console.WriteLine();
        }
    }
}
