using System;

using CheckersCommon.Parameters;
using CheckersCommon.Results;
using CheckersCommon.Utilities;
using Newtonsoft.Json;
using SimpleTCP;

namespace CheckersCommon.Models
{
    internal interface IGameService
    {
        string CreateRoom();
        void StartGame();
        void CloseRoom();
        void JoinRoom(string roomId);
        void LeaveRoom();
        void Surrender();
        void MakeMove();

        event EventHandler<EventArgs> PlayerWithdrew;
        event EventHandler<EventArgs> PlayerLeft;
        event EventHandler<EventArgs> PlayerJoined;
        event EventHandler<EventArgs> GameStarted;
        event EventHandler<EventArgs> UpdateGameboard;
    }

    internal sealed class GameService : IGameService
    {
        private string _sessionId;
        private readonly IGameClient _gameClient;

        public GameService(IGameClient gameClient)
        {
            _gameClient = gameClient.NotNull();
            _gameClient.DataReceived += OnDataReceived;
        }

        public event EventHandler<EventArgs> PlayerWithdrew;
        public event EventHandler<EventArgs> PlayerLeft;
        public event EventHandler<EventArgs> PlayerJoined;
        public event EventHandler<EventArgs> GameStarted;
        public event EventHandler<EventArgs> UpdateGameboard;

        public void CloseRoom()
        {
            _gameClient.Send(new CloseRoomParameter(_sessionId));

            _sessionId = null;
        }

        private void OnDataReceived(object sender, Message message)
        {
            Parameter parameter = JsonConvert.DeserializeObject<Parameter>(message.MessageString);

            if(parameter.ActionType == ActionType.UpdateGameboard)
            {
                UpdateGameboard?.Invoke(this, EventArgs.Empty);
            }
            else if(parameter.ActionType == ActionType.JoinRoom)
            {
                PlayerJoined?.Invoke(this, EventArgs.Empty);
            }
            else if(parameter.ActionType == ActionType.LeaveRoom)
            {
                PlayerLeft?.Invoke(this, EventArgs.Empty);
            }
            else if(parameter.ActionType == ActionType.StartGame)
            {
                GameStarted?.Invoke(this, EventArgs.Empty);
            }
            else if(parameter.ActionType == ActionType.Surrender)
            {
                PlayerWithdrew?.Invoke(this, EventArgs.Empty);
            }
        }

        public void JoinRoom(string roomId)
        {
            JoinRoomResult result = _gameClient.Get<JoinRoomParameter, JoinRoomResult>(new JoinRoomParameter { RoomId = roomId });

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

        public void StartGame()
        {
            _gameClient.Send(new NewGameParameter(_sessionId));
        }

        public string CreateRoom()
        {
            CreateRoomResult result = _gameClient.Get<CreateRoomParameter, CreateRoomResult>(new CreateRoomParameter());

            _sessionId = result.SessionId;

            return result.RoomId;
        }

        public void Surrender()
        {
            _gameClient.Send(new SurrenderParameter(_sessionId));
        }
    }
}
