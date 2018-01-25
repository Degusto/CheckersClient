using Newtonsoft.Json;

namespace CheckersCommon.Models
{
    public sealed class Position
    {
        [JsonProperty(PropertyName = "row")]
        public int Row { get; set; }

        [JsonProperty(PropertyName = "column")]
        public int Column { get; set; }
    }
}
