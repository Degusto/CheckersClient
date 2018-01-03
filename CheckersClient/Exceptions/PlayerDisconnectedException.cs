namespace CheckersClient.Exceptions
{
    public sealed class PlayerDisconnectedException : ServerException
    {
        public PlayerDisconnectedException() : base("Player disconnected")
        {
        }
    }
}
