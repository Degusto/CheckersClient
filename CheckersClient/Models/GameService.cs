using System;

using CheckersCommon.Parameters;
using CheckersCommon.Results;
using CheckersCommon.Utilities;
using SimpleTCP;

namespace CheckersCommon.Models
{
    internal interface IGameService
    {
        void NewRoom();
        void NewGame();
        void CloseRoom();
        void JoinRoom(string roomId);
        void LeaveRoom();
        void OfferDraw();
        void Surrender();
        void MakeMove();

        event EventHandler<EventArgs> UpdateGameboard;
        event EventHandler<EventArgs> DrawRequest;
    }

    internal sealed class GameService : IGameService
    {
        private string _sessionId;
        private readonly IGameClient _gameClient;

        public GameService(IGameClient gameClient)
        {
            _gameClient = gameClient.NotNull();
        }

        public string RoomId { get; private set; }

        public event EventHandler<EventArgs> UpdateGameboard;
        public event EventHandler<EventArgs> DrawRequest;

        public void CloseRoom()
        {
            _gameClient.Send(new CloseRoomParameter(_sessionId));
            _gameClient.DataReceived += OnDataReceived;

            _sessionId = null;
        }

        private void OnDataReceived(object sender, Message e)
        {
            
        }

        public void JoinRoom(string roomId)
        {
            JoinRoomResult result = _gameClient.Get<JoinRoomParameter, JoinRoomResult>(new JoinRoomParameter(roomId));

            _sessionId = result.SessionId;
        }

        public void LeaveRoom()
        {
            _gameClient.Send(new LeaveRoomParameter(_sessionId));

            _sessionId = null;
        }

        public void MakeMove()
        {
            throw new NotImplementedException();
        }

        public void NewGame()
        {
            throw new NotImplementedException();
        }

        public void NewRoom()
        {
            NewRoomResult result = _gameClient.Get<NewRoomParameter, NewRoomResult>(new NewRoomParameter());

            _sessionId = result.SessionId;
            RoomId = result.RoomId;
        }

        public void OfferDraw()
        {
            throw new NotImplementedException();
        }

        public void Surrender()
        {
            _gameClient.Send(new SurrenderParameter());
        }
    }
}
