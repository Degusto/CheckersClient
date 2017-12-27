namespace CheckersCommon.Parameters
{
    public sealed class JoinRoomParameter : Parameter
    {
        public JoinRoomParameter(string sessionId) : base(sessionId, ActionType.JoinRoom)
        {
        }

        public string RoomId { get; set; }
    }
}
