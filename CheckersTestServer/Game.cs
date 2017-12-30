using System.Net.Sockets;

namespace CheckersTestServer
{
    internal sealed class Game
    {
        internal string HostSessionId { get; set; }

        internal TcpClient Host { get; set; }

        internal string GuestSessionId { get; set; }

        internal TcpClient Guest { get; set; }

        internal string RoomId { get; set; }

        internal bool GameInProgress { get; set; }
    }
}
