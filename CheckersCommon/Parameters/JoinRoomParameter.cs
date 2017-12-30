namespace CheckersCommon.Parameters
{
    public sealed class JoinRoomParameter : Parameter
    {
        public JoinRoomParameter() : base(null, ActionType.JoinRoom)
        {
        }

        public string RoomId { get; set; }
    }
}
