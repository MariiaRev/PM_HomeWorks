using System.Collections.Generic;
using Newtonsoft.Json;

namespace PrimeNumbersSearch_Threads
{
    public class Result
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("error")]
        public string Error { get; set; }

        [JsonProperty("duration")]
        public string Duration { get; set; }

        [JsonProperty("primes")]
        public List<int> Primes { get; set; }
    }
}
