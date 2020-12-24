using System;
using System.IO;
using Library.ERPReports;

namespace ERPReportsBot
{
    class Program
    {
        static int Main(string[] args)
        {
            //set concole width for good formatting
            Console.BufferWidth = Console.WindowWidth = 90;


            //print greetings
            Console.WriteLine($"{"",31}This is Homework 3 Task 2.1.\n{"",37}ERP Reports Bot");
            Console.WriteLine($"\n{"",30}### Made by Mariia Revenko ###\n\n");


            //print RULES
            Console.WriteLine("Here you can view various reports on products and their balances in warehouses.");
            Console.WriteLine("You can select a report to view or 'exit' command by entering corresponding number.\n");

            
            try
            {
                Menu.Start();                                                   
            }
            catch (FileNotFoundException)                                                   //csv file was not found
            {
                Console.WriteLine("\n\nOne of the 'database' files was not found.\n\n\n");
                return -1;                                                                      //incorrect program execution
            }
            catch (ProductException ex)                                                     //file with products is empty
            {
                Console.WriteLine($"\n\n{ex.Message}\n\n\n");
                return -1;                                                                      //incorrect program execution
            }
            catch (Exception ex)                                                            //unexpected exception
            {
                Console.WriteLine(ex.GetType());
                Console.WriteLine(ex.Message);
                return -1;                                                                      //incorrect program execution
            }


            Console.WriteLine("\n\n\n");
            return 0;                                                                           //correct program execution
        }
    }
}
