using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;

using CheckersCommon.Extensions;
using CheckersCommon.Models;

namespace CheckersTestServer
{
    internal sealed class Player
    {
        private Socket _client;

        public Player() : this(null, null) { }

        public Player(string sessionId, Socket client)
        {
            _client = client;
            SessionId = sessionId;

            if(_client != null)
            {
                _client.NoDelay = true;
            }
        }

        internal string SessionId { get; }

        internal void SendMessage(string json) => _client.Send(json);
    }
}
