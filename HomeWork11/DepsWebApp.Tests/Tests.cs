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
        // rates/{srcCurrency}/{dstCurrency} with different valid currencies
        public static async Task<(bool, string)> Test1(HttpClient client)
        {
            var testName = "Rates with different valid currencies";
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

        // rates/{srcCurrency}/{dstCurrency} with the same currencies
        public static async Task<(bool, string)> Test2(HttpClient client)
        {
            var testName = "Rates with the same currencies";
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

        // rates/{srcCurrency}/{dstCurrency} with the same currencies and some amount
        public static async Task<(bool, string)> Test3(HttpClient client)
        {
            var testName = "Rates with the same currencies and some amount";
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

        // rates/{srcCurrency}/{dstCurrency} with invalid currencies
        public static async Task<(bool, string)> Test4(HttpClient client)
        {
            var testName = "Rates with invalid currencies";
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

        // auth/register
        public static async Task<(bool, string)> Test5(HttpClient client)
        {
            var testName = "Registration";
            var request = "auth/register";
            var expectedStatusCode = HttpStatusCode.OK;
            var expectedContent = $"not implemented";

            var user = new User() { Login = "login1", Password = "1111111" };
            var json = JsonSerializer.Serialize(user);
            var buffer = Encoding.UTF8.GetBytes(json);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(request, byteContent);
            var actualContent = await response.Content.ReadAsStringAsync();
            var actualStatusCode = response.StatusCode;

            var passed = actualStatusCode == expectedStatusCode && actualContent.Contains(expectedContent);
            var resultString = GetResultString(testName, passed, request, expectedStatusCode, actualStatusCode, $"contains '{expectedContent}'", actualContent);

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
    }
}
