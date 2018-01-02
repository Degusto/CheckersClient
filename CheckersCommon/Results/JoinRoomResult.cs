using Newtonsoft.Json;

namespace CheckersCommon.Results
{
    public sealed class JoinRoomResult : Result
    {
        [JsonProperty(PropertyName = "session_id")]
        public string SessionId { get; set; }
    }
}
