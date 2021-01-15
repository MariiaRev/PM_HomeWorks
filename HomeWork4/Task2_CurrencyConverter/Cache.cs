using System;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace Task2_CurrencyConverter
{
    class Cache
    {
        [JsonProperty("rate")]
        public decimal Rate { get; set; }

        [JsonProperty("cc")]
        public string CurrencyCode { get; set; }

        [JsonProperty("exchangedate")]
        public DateTime ExchangeDate { get; set; }

    }
}
