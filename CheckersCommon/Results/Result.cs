using Newtonsoft.Json;

namespace CheckersCommon.Results
{
    public class Result
    {
        [JsonConstructor]
        protected Result()
        {
            Status = "OK";
        }

        [JsonIgnore]
        public bool Success
        {
            get
            {
                return Status == "OK";
            }
        }

        [JsonProperty(PropertyName = "status")]
        public string Status { get; private set; }

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

        //public static implicit operator string(Result result)
        //{
        //    return JsonConvert.SerializeObject(result);
        //}
    }
}
