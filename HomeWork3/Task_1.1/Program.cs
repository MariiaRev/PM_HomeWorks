using System;
using Library;


namespace Task_1._1
{
    class Program
    {
        //array's statistics with Linq
        static void Main(string[] args)
        {
            //print greetings
            Console.WriteLine($"{"",22}This is Homework 3 Task 1.1.\n{"",23}Array statistics with LINQ");
            Console.WriteLine($"\n{"",21}### Made by Mariia Revenko ###\n\n");


            //print RULES
            Console.WriteLine("Enter an array of comma separated integers and get array statistics.");
            Console.WriteLine("Enter \"exit\" to exit the program.");

            //start chat bot
            ArrayStatisticsBot.Start();




            Console.WriteLine("\n\n\n");
            //Console.Write("Enter any key: ");
            //Console.ReadKey();
        }
    }
}
