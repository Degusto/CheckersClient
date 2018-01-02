using Newtonsoft.Json;

namespace CheckersCommon.Models
{
    public sealed class Move
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "will_take_pawn")]
        public bool WillTakePawn { get; set; }

        [JsonProperty("destination_position")]
        public Position DestinationPosition { get; set; }
    }
}
