using System.Collections.Generic;
using CheckersCommon.Enums;
using CheckersCommon.Models;
using Newtonsoft.Json;

namespace CheckersCommon.Parameters
{
    public sealed class UpdateGameboardParameter : Parameter
    {
        public UpdateGameboardParameter() : base(null, ActionType.UpdateGameboard)
        {
        }

        [JsonProperty(PropertyName = "game_status")]
        public GameStatus GameStatus { get; set; }

        [JsonProperty(PropertyName = "pawns")]
        public IEnumerable<Pawn> Pawns { get; set; }
    }
}
