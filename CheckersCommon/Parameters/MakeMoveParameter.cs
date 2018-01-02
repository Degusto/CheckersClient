using CheckersCommon.Models;
using Newtonsoft.Json;

namespace CheckersCommon.Parameters
{
    public sealed class MakeMoveParameter : Parameter
    {
        public MakeMoveParameter(string sessionId) : base(sessionId, ActionType.MakeMove)
        {
        }

        [JsonProperty(PropertyName = "move")]
        public Move Move { get; set; }
    }
}
