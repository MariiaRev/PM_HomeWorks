using System;
using System.Net.Http;
using System.Threading.Tasks;
using RequestProcessor.App.Menu;
using RequestProcessor.App.Services;
using RequestProcessor.App.Logging;

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
                var client = new HttpClient
                {
                    Timeout = TimeSpan.FromSeconds(10)
                };
                var requestHandler = new RequestHandler(client);
                var responseHandler = new ResponseHandler();
                var logger = new Logger();
                var performer = new RequestPerformer(requestHandler, responseHandler, logger);
                
                var optionsPath = "options.json";
                var options = new OptionsSource(optionsPath);

                var mainMenu = new MainMenu(performer, options, logger);
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
