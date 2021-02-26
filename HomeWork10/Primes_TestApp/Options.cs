using System.Text.Json.Serialization;

namespace Primes_TestApp
{
    public class Options
    {
        [JsonPropertyName("baseAddress")]
        public string BaseAddress { get; set; } 
    }
}
