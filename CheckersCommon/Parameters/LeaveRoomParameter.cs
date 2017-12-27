namespace CheckersCommon.Parameters
{
    public sealed class LeaveRoomParameter : Parameter
    {
        public LeaveRoomParameter(string sessionId) : base(sessionId, ActionType.LeaveRoom)
        {
        }
    }
}
