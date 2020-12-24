using System;
using System.Collections.Generic;
using Library;

namespace Task_1._3
{
    class Program
    {
        static void Main(string[] args)
        {
            //print greetings
            Console.WriteLine($"{"",22}This is Homework 3 Task 1.3.\n{"",25}Working with Dictionary");
            Console.WriteLine($"\n{"",21}### Made by Mariia Revenko ###\n\n");


            //print RULES
            Console.WriteLine("Enter dictionary elements number. Then enter each element one by one.");
            Console.WriteLine("Enter \"exit\" to exit the program.");

            int N;

            while (true)
            {
                AcceptElementsNumberFromUser("\n\n\n\nEnter number of the elements or the \"exit\" command, please: ", out string userInput);

                while (!int.TryParse(userInput, out N) || N <= 0)
                {
                    if (userInput == "exit")
                    {
                        Console.WriteLine("\n\nYou exit the program. Bye!\n\n\n");
                        return;
                    }

                    AcceptElementsNumberFromUser("\n\nNot an integer number > 0. Try again: ", out userInput);
                }

                Dictionary<Region, RegionSettings> userDict = new Dictionary<Region, RegionSettings>(N);

                for(int i=1; i<=N; i++)
                {
                    bool success;
                    do
                    {
                        Console.WriteLine($"\n\nDictionary element #{i}");
                        AcceptRegionFromUser(out Region region);
                        AcceptRegionSettingsFromUser(out RegionSettings regionSettings);

                        success = userDict.TryAdd(region, regionSettings);

                        if (!success)
                        {
                            Console.WriteLine($"\n\nThe pair with the key <{region.Brand}, {region.Country}> is already " +
                                $"in the dictionary. Please, try to enter the dictionary element again.");
                        }

                    } while (!success);
                }


                Console.WriteLine($"\n\n\n{"", 28}{"ENTERED DICTIONARY"}");
                Console.WriteLine($"\n{"", 13}[Brand]{"", 14}[Country]{"", 14}[Website]\n");

                foreach(var elem in userDict)
                {
                    Console.WriteLine($"{elem.Key.Brand, 20}   {elem.Key.Country, 20}   {elem.Value.WebSite, 20}");
                }
            }
        }

        static void AcceptElementsNumberFromUser(string message, out string userInput)
        {
            Console.Write(message);
            userInput = Console.ReadLine();
            Console.WriteLine();
        }

        static void AcceptRegionFromUser(out Region region)
        {
            string brand, country;

            Console.Write($"\nEnter {nameof(region.Brand)}: ");
            brand = Console.ReadLine();
            Console.Write($"\nEnter {nameof(region.Country)}: ");
            country = Console.ReadLine();

            region = new Region(brand, country);
        }

        static void AcceptRegionSettingsFromUser(out RegionSettings regionSettings)
        {
            string site;

            Console.Write($"\nEnter {nameof(regionSettings.WebSite)}: ");
            site = Console.ReadLine();
            Console.WriteLine();

            regionSettings = new RegionSettings(site);
        }
    }
}
