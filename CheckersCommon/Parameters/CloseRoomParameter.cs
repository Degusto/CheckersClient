namespace CheckersCommon.Parameters
{
    public sealed class CloseRoomParameter : Parameter
    {
        public CloseRoomParameter(string sessionId) : base(sessionId, ActionType.CloseRoom)
        {
        }
    }
}