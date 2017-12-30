using Newtonsoft.Json;

namespace CheckersCommon.Results
{
    public sealed class CreateRoomResult : Result
    {
        [JsonProperty(PropertyName = "session_id")]
        public string SessionId { get; set; }

        [JsonProperty(PropertyName = "room_id")]
        public string RoomId { get; set; }
    }
}
