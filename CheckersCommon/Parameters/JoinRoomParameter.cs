using Newtonsoft.Json;

namespace CheckersCommon.Parameters
{
    public sealed class JoinRoomParameter : Parameter
    {
        public JoinRoomParameter() : base(null, ActionType.JoinRoom)
        {
        }

        [JsonProperty(PropertyName="room_id")]
        public string RoomId { get; set; }
    }
}
