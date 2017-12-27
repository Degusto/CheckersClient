using Newtonsoft.Json;

namespace CheckersCommon.Parameters
{
    public class Parameter
    {
        [JsonConstructor]
        private Parameter()
        {

        }

        protected Parameter(string sessionId, ActionType actionType)
        {
            SessionId = sessionId;
            ActionType = actionType;
        }

        [JsonProperty(PropertyName = "session_id")]
        public string SessionId { get; private set; }

        [JsonProperty(PropertyName = "action_type")]
        public ActionType ActionType { get; private set; }
    }
}
