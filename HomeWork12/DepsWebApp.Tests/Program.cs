using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using static DepsWebApp.Tests.Tests;

namespace DepsWebApp.Tests
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("This is Homework 11 Task 2. Tests for Web Service DepsWebApp\nMade by Mariia Revenko");

            var optionsPath = "options.json";
            try
            {
                var baseAddress = await GetBaseAddress(optionsPath);
                var client = new HttpClient
                {
                    BaseAddress = new Uri(baseAddress)
                };

                var results = new List<(bool, string)>()
                {
                    await Test1(client),
                    await Test2(client),
                    await Test3(client),
                    await Test4(client),
                    await Test5(client)
                };

                results.ForEach(result => Console.WriteLine(result.Item2));

                var passed = results.Count(res => res.Item1);
                var failed = results.Count(res => !res.Item1);
                Console.WriteLine($"\n\nPassed tests: {passed}/{results.Count}");
                Console.WriteLine($"Failed tests: {failed}/{results.Count}");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"The file {optionsPath} was not found.\nTests failed.");
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine($"Serialization exception. Check the file {optionsPath}.\nTests failed.");
            }
            catch (HttpRequestException)
            {
                Console.WriteLine($"Server is not available.\nTests failed.");
            }

            Console.WriteLine("\n\n\n");
        }

        static async Task<string> GetBaseAddress(string path)
        {
            var text = await File.ReadAllTextAsync(path);
            var json = JsonSerializer.Deserialize<Options>(text);

            if (json.BaseAddress == null)
                throw new ArgumentNullException(nameof(json.BaseAddress), "Base address cannot be null");

            return json.BaseAddress;
        }
    }
}
