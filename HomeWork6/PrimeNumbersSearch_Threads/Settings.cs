using Newtonsoft.Json;

namespace PrimeNumbersSearch_Threads
{
    public class Settings
    {
        [JsonProperty("primesFrom")]
        public int PrimesFrom { get; set; }

        [JsonProperty("primesTo")]
        public int PrimesTo { get; set; }
    }
}
