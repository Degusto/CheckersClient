namespace CheckersCommon.Parameters
{
    public sealed class StartGameParameter : Parameter
    {
        public StartGameParameter(string sessionId) : base(sessionId, ActionType.StartGame)
        {
        }
    }
}
