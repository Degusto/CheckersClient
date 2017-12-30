namespace CheckersCommon.Parameters
{
    public sealed class SurrenderParameter : Parameter
    {
        public SurrenderParameter(string sessionId) : base(sessionId, ActionType.Surrender)
        {
        }
    }
}
