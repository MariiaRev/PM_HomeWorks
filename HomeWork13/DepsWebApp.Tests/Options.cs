using System.Text.Json.Serialization;

namespace DepsWebApp.Tests
{
    public class Options
    {
        [JsonPropertyName("baseAddress")]
        public string BaseAddress { get; set; }
    }
}
