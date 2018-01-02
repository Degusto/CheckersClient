using System.Collections.Generic;
using CheckersCommon.Enums;
using Newtonsoft.Json;

namespace CheckersCommon.Models
{
    public sealed class Pawn
    {
        [JsonProperty(PropertyName = "position")]
        public Position Position { get; set; }

        [JsonProperty(PropertyName = "available_moves")]
        public IEnumerable<Position> AvailableMoves { get; set; }

        [JsonProperty(PropertyName = "owner")]
        public PlayerType Owner { get; set; }
    }
}
