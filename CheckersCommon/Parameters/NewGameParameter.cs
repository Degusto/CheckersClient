namespace CheckersCommon.Parameters
{
    public sealed class NewGameParameter : Parameter
    {
        public NewGameParameter(string sessionId) : base(sessionId, ActionType.StartGame)
        {
        }
    }
}
