using System.Collections.Generic;
using CheckersCommon.Enums;
using Newtonsoft.Json;

namespace CheckersCommon.Models
{
    public sealed class Pawn
    {
        public Pawn()
        {
            AvailableMoves = new List<Move>();
        }

        [JsonProperty(PropertyName = "position")]
        public Position Position { get; set; }

        [JsonProperty(PropertyName = "available_moves")]
        public IList<Move> AvailableMoves { get; set; }

        [JsonProperty(PropertyName = "owner")]
        public PlayerType Owner { get; set; }

        [JsonProperty(PropertyName = "is_promoted")]
        public bool IsPromoted { get; set; }
    }
}