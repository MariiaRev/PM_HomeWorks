using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DepsWebApp.Tests
{
    public static class Tests
    {
        // auth/register with invalid login/password
        public static async Task<(bool, string)> Test1(HttpClient client)
        {
            var testName = "Registration with invalid login and password";
            var request = "auth/register";
            var expectedStatusCode = HttpStatusCode.BadRequest;

            (string login, string password) = ("login", null);
            var byteContent = SetByteUserContent(login, password);
            var response = await client.PostAsync(request, byteContent);
            var actualStatusCode = response.StatusCode;

            var passed = actualStatusCode == expectedStatusCode;
            var resultString = GetResultString(testName, passed, 
                request + $"\nwith login: \"{login ?? "null"}\" and password: {password ?? "null"}\n", expectedStatusCode, actualStatusCode);

            return (passed, resultString);
        }

        // auth/register with valid login and password
        public static async Task<(bool, string)> Test2(HttpClient client)
        {
            var testName = "Registration with valid login and password";
            var request = "auth/register";
            var expectedStatusCode = HttpStatusCode.OK;

            (string login, string password) = ($"login {DateTime.Now}", "1111111");
            var byteContent = SetByteUserContent(login, password);
            var response = await client.PostAsync(request, byteContent);
            var actualStatusCode = response.StatusCode;

            var passed = actualStatusCode == expectedStatusCode;
            var resultString = GetResultString(testName, passed, 
                request + $"\nwith login: \"{login ?? "null"}\" and password: {password ?? "null"}\n", expectedStatusCode, actualStatusCode);

            return (passed, resultString);
        }

        // auth/register with valid login and password when user already exists
        public static async Task<(bool, string)> Test3(HttpClient client, string login, string password)
        {
            var testName = "Registration with valid login and password but with existed login";
            var request = "auth/register";
            var expectedStatusCode = HttpStatusCode.Conflict;

            // register user
            var byteContent1 = SetByteUserContent(login, password);
            await client.PostAsync(request, byteContent1);

            // try to register user with the same login
            var password2 = "another password";
            var byteContent2 = SetByteUserContent(login, password2);
            var response = await client.PostAsync(request, byteContent2);
            var actualStatusCode = response.StatusCode;

            var passed = actualStatusCode == expectedStatusCode;
            var resultString = GetResultString(testName, passed,
                request + $"\nwith login: \"{login ?? "null"}\" and password: {password2 ?? "null"}" +
                $"\nafter succesful registration login: \"{login ?? "null"}\" and password: {password ?? "null"}\n", expectedStatusCode, actualStatusCode);

            return (passed, resultString);
        }

        // rates/{srcCurrency}/{dstCurrency} unauthorized
        public static async Task<(bool, string)> Test4(HttpClient client)
        {
            var testName = "Rates by unauthorized user";
            var srcCurrency = "usd";
            var dstCurrency = "uah";
            var request = $"rates/{srcCurrency}/{dstCurrency}";
            var expectedStatusCode = HttpStatusCode.Unauthorized;

            var response = await client.GetAsync(request);
            var actualStatusCode = response.StatusCode;

            var passed = actualStatusCode == expectedStatusCode;
            var resultString = GetResultString(testName, passed, request, expectedStatusCode, actualStatusCode);

            return (passed, resultString);
        }

        // rates/{srcCurrency}/{dstCurrency} with different valid currencies authorized
        public static async Task<(bool, string)> Test5(HttpClient client)
        {
            var testName = "Rates with different valid currencies by authorized user";
            var srcCurrency = "usd";
            var dstCurrency = "uah";
            var request = $"rates/{srcCurrency}/{dstCurrency}";
            var expectedStatusCode = HttpStatusCode.OK;

            var response = await client.GetAsync(request);
            var actualContent = await response.Content.ReadAsStringAsync();
            var actualStatusCode = response.StatusCode;

            var passed = actualStatusCode == expectedStatusCode;
            var resultString = GetResultString(testName, passed, request, expectedStatusCode, actualStatusCode, null, actualContent);

            return (passed, resultString);
        }

        // rates/{srcCurrency}/{dstCurrency} with the same currencies authorized
        public static async Task<(bool, string)> Test6(HttpClient client)
        {
            var testName = "Rates with the same currencies by authorized user";
            var srcCurrency = "usd";
            var dstCurrency = "usd";
            var request = $"rates/{srcCurrency}/{dstCurrency}";
            var expectedStatusCode = HttpStatusCode.OK;
            var expectedContent = "1";

            var response = await client.GetAsync(request);
            var actualContent = await response.Content.ReadAsStringAsync();
            var actualStatusCode = response.StatusCode;

            var passed = actualStatusCode == expectedStatusCode && expectedContent == actualContent;
            var resultString = GetResultString(testName, passed, request, expectedStatusCode, actualStatusCode, expectedContent, actualContent);

            return (passed, resultString);
        }

        // rates/{srcCurrency}/{dstCurrency} with the same currencies and some amount authorized
        public static async Task<(bool, string)> Test7(HttpClient client)
        {
            var testName = "Rates with the same currencies and some amount by authorized user";
            var srcCurrency = "usd";
            var dstCurrency = "usd";
            var amount = 10000d;
            var request = $"rates/{srcCurrency}/{dstCurrency}?amount={amount}";
            var expectedStatusCode = HttpStatusCode.OK;
            var expectedContent = $"{amount}";

            var response = await client.GetAsync(request);
            var actualContent = await response.Content.ReadAsStringAsync();
            var actualStatusCode = response.StatusCode;

            var passed = actualStatusCode == expectedStatusCode && expectedContent == actualContent;
            var resultString = GetResultString(testName, passed, request, expectedStatusCode, actualStatusCode, expectedContent, actualContent);

            return (passed, resultString);
        }

        // rates/{srcCurrency}/{dstCurrency} with invalid currencies authorized
        public static async Task<(bool, string)> Test8(HttpClient client)
        {
            var testName = "Rates with invalid currencies by authorized user";
            var srcCurrency = "usd";
            var dstCurrency = "udd";
            var request = $"rates/{srcCurrency}/{dstCurrency}?amount=1000";
            var expectedStatusCode = HttpStatusCode.BadRequest;

            var response = await client.GetAsync(request);
            var actualContent = await response.Content.ReadAsStringAsync();
            var actualStatusCode = response.StatusCode;

            var passed = actualStatusCode == expectedStatusCode;
            var resultString = GetResultString(testName, passed, request, expectedStatusCode, actualStatusCode, null, actualContent);

            return (passed, resultString);
        }

        private static string GetResultString(string testName, bool passed, string testRequest, HttpStatusCode expectedCode, HttpStatusCode actualCode, 
            string expectedContent = null, string actualContent = null)
        {
            expectedContent = expectedContent == null ? "" : $"Expected content: {expectedContent}\n  ";
            actualContent = actualContent == null ? "" : $"Actual content: {actualContent}\n";
            var passedMessage = passed ? "\n\nTest passed\n" : "\n\nTest failed\n";

            return new StringBuilder()
                .Append($"\n\n\t\tTEST: {testName}\n")
                .Append($"\n\nTesting request: {testRequest}\n")
                .Append($"Expected status code: {expectedCode}\n")
                .Append($"  Actual status code: {actualCode}\n")
                .Append(expectedContent)
                .Append(actualContent)
                .Append(passedMessage)
                .Append("---------------------------------------------------------------------------------------")
                .ToString();
        }

        private static ByteArrayContent SetByteUserContent(string login, string password)
        {
            var user = new User() { Login = login, Password = password };
            var json = JsonSerializer.Serialize(user);
            var buffer = Encoding.UTF8.GetBytes(json);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            
            return byteContent;
        }
    }
}
