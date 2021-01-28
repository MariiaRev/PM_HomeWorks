using Newtonsoft.Json;

namespace UniqueLoginsIssuingService
{
    public class Result
    {
        [JsonProperty("successful")]
        public int SuccessfulAttempts { get; set; }
        [JsonProperty("failed")]
        public int FailedAttempts { get; set; }

        public Result(int successful, int failed)
        {
            SuccessfulAttempts = successful;
            FailedAttempts = failed;
        }
    }
}
