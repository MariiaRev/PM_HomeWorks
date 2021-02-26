using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Primes_TestApp
{
    class Program
    {
        static async Task Main()
        {
            var optionsPath = "options.json";
            try
            {
                var baseAddress = await GetBaseAddress(optionsPath);
                var client = new HttpClient
                {
                    BaseAddress = new Uri(baseAddress)
                };

                var results = new List<bool>()
                {
                    await Test1(client),
                    await Test2(client),
                    await Test3(client),
                    await Test4(client),
                    await Test5(client),
                    await Test6(client)
                };

                var passed = results.Where(res => res == true).Count();
                var failed = results.Where(res => res == false).Count();
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
             
        // test the path '/'
        static async Task<bool> Test1(HttpClient client)
        {
            var request = "/";
            var authorName = "Mariia Revenko";
            var appName = "Prime numbers";
            var expectedStatusCode = HttpStatusCode.OK;

            var response = await client.GetAsync(request);
            var actualContent = await response.Content.ReadAsStringAsync();
            var actualStatusCode = response.StatusCode;
            
            Console.WriteLine($"\n\t\tTEST: The author's and the app names");
            Console.WriteLine($"\n\nTesting request: {request}");
            Console.WriteLine($"Expected message: contains '{authorName}' and '{appName}'");
            Console.WriteLine($"  Actual message:\n{actualContent}");
            Console.WriteLine($"Expected status code: {expectedStatusCode}");
            Console.WriteLine($"  Actual status code: {actualStatusCode}");

            if (expectedStatusCode == actualStatusCode &&
                actualContent.Contains(authorName, StringComparison.OrdinalIgnoreCase) &&
                actualContent.Contains(appName, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("\n\nTest passed.");
                Console.WriteLine("----------------------------------------------------------------------------------");
                return true;
            }
            else
            {
                Console.WriteLine("\n\nTest failed.");
                Console.WriteLine("----------------------------------------------------------------------------------");
                return false;
            }
        }

        // test the path '/primes/{number:int}' with a known prime number
        static async Task<bool> Test2(HttpClient client)
        {
            var request = "/primes/17";
            var expectedContent = "is prime";
            var expectedStatusCode = HttpStatusCode.OK;

            var response = await client.GetAsync(request);
            var actualContent = await response.Content.ReadAsStringAsync();
            var actualStatusCode = response.StatusCode;

            Console.WriteLine($"\n\t\tTEST: A known prime number");
            Console.WriteLine($"\n\nTesting request: {request}");
            Console.WriteLine($"Expected message: contains '{expectedContent}'");
            Console.WriteLine($"  Actual message: '{actualContent}'");
            Console.WriteLine($"Expected status code: {expectedStatusCode}");
            Console.WriteLine($"  Actual status code: {actualStatusCode}");

            if (expectedStatusCode == actualStatusCode &&
                actualContent.Contains(expectedContent, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("\n\nTest passed.");
                Console.WriteLine("----------------------------------------------------------------------------------");
                return true;
            }
            else
            {
                Console.WriteLine("\n\nTest failed.");
                Console.WriteLine("----------------------------------------------------------------------------------");
                return false;
            }
        }

        // test the path '/primes/{number:int}' with a known non-prime number
        static async Task<bool> Test3(HttpClient client)
        {
            var request = "/primes/22";
            var expectedContent = "is not prime";
            var expectedStatusCode = HttpStatusCode.NotFound;

            var response = await client.GetAsync(request);
            var actualContent = await response.Content.ReadAsStringAsync();
            var actualStatusCode = response.StatusCode;

            Console.WriteLine($"\n\t\tTEST: A known non-prime number");
            Console.WriteLine($"\n\nTesting request: {request}");
            Console.WriteLine($"Expected message: contains '{expectedContent}'");
            Console.WriteLine($"  Actual message: '{actualContent}'");
            Console.WriteLine($"Expected status code: {expectedStatusCode}");
            Console.WriteLine($"  Actual status code: {actualStatusCode}");

            if (expectedStatusCode == actualStatusCode &&
                actualContent.Contains(expectedContent, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("\n\nTest passed.");
                Console.WriteLine("----------------------------------------------------------------------------------");
                return true;
            }
            else
            {
                Console.WriteLine("\n\nTest failed.");
                Console.WriteLine("----------------------------------------------------------------------------------");
                return false;
            }
        }

        // test the path '/primes' with a known range of primes
        static async Task<bool> Test4(HttpClient client)
        {
            var request = "/primes?from=0&to=10";
            var expectedContent = new int[] { 2, 3, 5, 7 };
            var expectedStatusCode = HttpStatusCode.OK;

            var response = await client.GetAsync(request);
            var content = await response.Content.ReadAsStringAsync();
            var actualStatusCode = response.StatusCode;
            var actualContent = content.Split(new char[] { ',', '[', ']' }, StringSplitOptions.RemoveEmptyEntries)
                                       .Select(n => int.Parse(n)).ToArray();

            Console.WriteLine($"\n\t\tTEST: A known range of primes");
            Console.WriteLine($"\n\nTesting request: {request}");
            Console.WriteLine($"Expected content: '[{string.Join(',', expectedContent)}]'");
            Console.WriteLine($"  Actual content: '[{string.Join(',', actualContent)}]'");
            Console.WriteLine($"Expected status code: {expectedStatusCode}");
            Console.WriteLine($"  Actual status code: {actualStatusCode}");

            if (expectedStatusCode == actualStatusCode &&
                actualContent.SequenceEqual(expectedContent))
            {
                Console.WriteLine("\n\nTest passed.");
                Console.WriteLine("----------------------------------------------------------------------------------");
                return true;
            }
            else
            {
                Console.WriteLine("\n\nTest failed.");
                Console.WriteLine("----------------------------------------------------------------------------------");
                return false;
            }
        }

        // test the path '/primes' with a known range of non-primes
        static async Task<bool> Test5(HttpClient client)
        {
            var request = "/primes?from=-10&to=1";
            var expectedContent = new int[] { };
            var expectedStatusCode = HttpStatusCode.OK;

            var response = await client.GetAsync(request);
            var content = await response.Content.ReadAsStringAsync();
            var actualStatusCode = response.StatusCode;
            var actualContent = content.Split(new char[] { ',', '[', ']' }, StringSplitOptions.RemoveEmptyEntries)
                                       .Select(n => int.Parse(n)).ToArray();

            Console.WriteLine($"\n\t\tTEST: A known range of non-primes");
            Console.WriteLine($"\n\nTesting request: {request}");
            Console.WriteLine($"Expected content: '[{string.Join(',', expectedContent)}]'");
            Console.WriteLine($"  Actual content: '[{string.Join(',', actualContent)}]'");
            Console.WriteLine($"Expected status code: {expectedStatusCode}");
            Console.WriteLine($"  Actual status code: {actualStatusCode}");

            if (expectedStatusCode == actualStatusCode &&
                actualContent.SequenceEqual(expectedContent))
            {
                Console.WriteLine("\n\nTest passed.");
                Console.WriteLine("----------------------------------------------------------------------------------");
                return true;
            }
            else
            {
                Console.WriteLine("\n\nTest failed.");
                Console.WriteLine("----------------------------------------------------------------------------------");
                return false;
            }
        }

        // test the path '/primes' when a range is not transmitted or transmitted incompletely or incorrectly 
        static async Task<bool> Test6(HttpClient client)
        {
            var request = "/primes?to=abcd";
            var expectedStatusCode = HttpStatusCode.BadRequest;

            var response = await client.GetAsync(request);
            var actualStatusCode = response.StatusCode;
            
            Console.WriteLine($"\n\t\tTEST: Invalid range");
            Console.WriteLine($"\n\nTesting request: {request}");
            Console.WriteLine($"Expected status code: {expectedStatusCode}");
            Console.WriteLine($"  Actual status code: {actualStatusCode}");

            if (expectedStatusCode == actualStatusCode)
            {
                Console.WriteLine("\n\nTest passed.");
                Console.WriteLine("----------------------------------------------------------------------------------");
                return true;
            }
            else
            {
                Console.WriteLine("\n\nTest failed.");
                Console.WriteLine("----------------------------------------------------------------------------------");
                return false;
            }
        }
    }
}
