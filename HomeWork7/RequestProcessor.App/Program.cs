using System;
using System.Threading.Tasks;
using RequestProcessor.App.Menu;

namespace RequestProcessor.App
{
    /// <summary>
    /// Entry point.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Entry point.
        /// </summary>
        /// <returns>Returns exit code.</returns>
        private static async Task<int> Main()
        {
            //print greetings
            Console.WriteLine($"{"",22}This is Homework 7 Task 1\n{"",26}Request Processor");
            Console.WriteLine($"\n{"",19}### Made by Mariia Revenko ###");
            Console.WriteLine($"{"",21}### and by 'colleague' ###\n\n");

            try
            {
                // ToDo: Null arguments should be replaced with correct implementations.
                var mainMenu = new MainMenu(null, null, null);
                return await mainMenu.StartAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Critical unhandled exception");
                Console.WriteLine(ex);
                return -1;
            }
        }
    }
}
