using System;
using System.Linq;
using System.Collections.Generic;
using Library;

namespace Task_1._2
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Player> playersCS = new List<Player>()
            {
                new Player("Ivan", "Ivanenko", 29, PlayerRank.Captain),
                new Player("Peter", "Petrenko", 19, PlayerRank.Private),
                new Player("Ivan", "Ivanov", 59, PlayerRank.Colonel),
                new Player("Ivan", "Snezko", 52, PlayerRank.Lieutenant),
                new Player("Alex", "Zeshko", 34, PlayerRank.Colonel),

                new Player("Vasiliy", "Sokol", 34, PlayerRank.Major),
                new Player("Ivan", "Ivanenko", 29, PlayerRank.Captain),             //dublicate
                new Player("Alex", "Alexeenko", 31, PlayerRank.Major),
                new Player("Eva", "White", 29, PlayerRank.Captain),
                new Player("Alice", "Brown", 42, PlayerRank.General),
               
                new Player("Peter", "Petrenko", 19, PlayerRank.Private),            //dublicate
                new Player("Harry", "Potter", 36, PlayerRank.Major),
                new Player("Ron", "Weasley", 36, PlayerRank.Colonel),
                new Player("Hermione", "Granger", 37, PlayerRank.Private),
                new Player("Piper", "Halliwell", 26, PlayerRank.Lieutenant),
            };


            //print greetings
            Console.WriteLine($"{"",22}This is Homework 3 Task 1.2.\n{"",16}Sorting. IComparer and IEqualityComparer");
            Console.WriteLine($"\n{"",21}### Made by Mariia Revenko ###\n\n");


            //sort distinct players by name
            var playersSortedByName = playersCS.Distinct(new PlayerEqualityComparer()).ToList();
            playersSortedByName.Sort(new PlayerNameComparer());

            //print results
            Console.WriteLine($"\n{"", 23}CS players sorted by name\n{"", 25}(lastname + firstname)\n");
            playersSortedByName.ForEach(Console.WriteLine);


            //sort distinct players by age
            Console.WriteLine($"\n\n\n{"", 23}CS players sorted by age\n");
            var playersSortedByAge = playersCS.Distinct(new PlayerEqualityComparer()).ToList();
            playersSortedByAge.Sort(new PlayerAgeComparer());

            //print results
            playersSortedByAge.ForEach(Console.WriteLine);

            //sort distinct players by rank
            Console.WriteLine($"\n\n\n{"", 23}CS players sorted by rank\n");
            var playersSortedByRank = playersCS.Distinct(new PlayerEqualityComparer()).ToList();
            playersSortedByRank.Sort(new PlayerRankComparer());

            //print results
            playersSortedByRank.ForEach(Console.WriteLine);







            Console.WriteLine("\n\n\n");
            //Console.Write("Enter any key: ");
            //Console.ReadKey();
        }
    }
}
