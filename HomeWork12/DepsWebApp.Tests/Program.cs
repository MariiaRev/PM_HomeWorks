using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using static DepsWebApp.Tests.Tests;
using System.Net.Http.Headers;
using System.Text;

namespace DepsWebApp.Tests
{
    class Program
    {
        private static async Task Main(string[] args)
        {
            Console.WriteLine("This is Homework 12 Task 2. Tests for Web Service DepsWebApp\nMade by Mariia Revenko");

            var optionsPath = "options.json";
            try
            {
                var baseAddress = await GetBaseAddress(optionsPath);

                // client for unauthorized user
                var client1 = new HttpClient
                {
                    BaseAddress = new Uri(baseAddress)
                };

                // client for authorized user
                (string login, string password) = ("login1", "1111111");
                var encodedUserData = EncodeUserData(login, password);
                var client2 = new HttpClient
                {
                    BaseAddress = new Uri(baseAddress)
                };
                client2.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", encodedUserData);
                
                var results = new List<(bool, string)>()
                {
                    await Test1(client1),
                    await Test2(client1),
                    await Test3(client1, login, password),
                    await Test4(client1),
                    await Test5(client2),
                    await Test6(client2),
                    await Test7(client2),
                    await Test8(client2)
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

        private static async Task<string> GetBaseAddress(string path)
        {
            var text = await File.ReadAllTextAsync(path);
            var json = JsonSerializer.Deserialize<Options>(text);

            if (json.BaseAddress == null)
                throw new ArgumentNullException(nameof(json.BaseAddress), "Base address cannot be null");

            return json.BaseAddress;
        }

        private static string EncodeUserData(string login, string password)
        {
            var str = $"{login}: {password}";
            var bytes = Encoding.UTF8.GetBytes(str);

            return Convert.ToBase64String(bytes);
        }
    }
}
