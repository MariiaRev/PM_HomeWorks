using System;

namespace Task3_Notes
{
    class Program
    {
        static void Main(string[] args)
        {
            //print greetings
            Console.WriteLine($"{"",22}This is Homework 4 Task 3\n{"", 32}Notes");
            Console.WriteLine($"\n{"",21}### Made by Mariia Revenko ###\n\n");

            
            var storagePath = "storage.json";

            Menu.Start(storagePath);
        }

        
    }
}
