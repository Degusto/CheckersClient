using System.Collections.Generic;
using System.Net.Sockets;
using CheckersCommon.Models;

namespace CheckersTestServer
{
    internal sealed class Player
    {
        private Socket _client;

        internal Socket Client
        {
            get => _client;
            set
            {
                _client = value;

                if (value != null)
                {
                    value.NoDelay = true;
                }
            }
        }

        internal string SessionId { get; set; }

        internal IEnumerable<Pawn> Pawns { get; set; }
    }
}
