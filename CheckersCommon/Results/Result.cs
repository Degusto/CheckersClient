using Newtonsoft.Json;

namespace CheckersCommon.Results
{
    public class Result
    {
        [JsonConstructor]
        protected Result()
        {

        }

        [JsonIgnore]
        public bool Success => Status == "OK";

        [JsonProperty(PropertyName = "status")]
        public string Status { get; private set; } = "OK";

        [JsonProperty(PropertyName = "error")]
        public string Error { get; private set; }

        public static Result CreateSuccess()
        {
            return new Result();
        }

        public static Result CreateError(string error)
        {
            return new Result
            {
                Error = error,
                Status = "FAIL"
            };
        }

        public static implicit operator string(Result result)
        {
            return JsonConvert.SerializeObject(result);
        }
    }
}
