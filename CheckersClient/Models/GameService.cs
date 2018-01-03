using System;

using CheckersCommon.Parameters;
using CheckersCommon.Results;
using CheckersCommon.Utilities;
using Newtonsoft.Json;

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
        void MakeMove(Move move);

        event EventHandler<EventArgs> PlayerLeft;
        event EventHandler<EventArgs> PlayerJoined;
        event EventHandler<EventArgs> GameStarted;
        event EventHandler<UpdateGameboardParameter> UpdateGameboard;
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

        public event EventHandler<EventArgs> PlayerLeft;
        public event EventHandler<EventArgs> PlayerJoined;
        public event EventHandler<EventArgs> GameStarted;
        public event EventHandler<UpdateGameboardParameter> UpdateGameboard;

        public void CloseRoom()
        {
            _gameClient.Send(new CloseRoomParameter(_sessionId));

            _sessionId = null;
        }

        private void OnDataReceived(object sender, string json)
        {
            Parameter parameter = JsonConvert.DeserializeObject<Parameter>(json);

            if (parameter.ActionType == ActionType.UpdateGameboard)
            {
                UpdateGameboard?.Invoke(this, JsonConvert.DeserializeObject<UpdateGameboardParameter>(json));
            }
            else if (parameter.ActionType == ActionType.JoinRoom)
            {
                PlayerJoined?.Invoke(this, EventArgs.Empty);
            }
            else if (parameter.ActionType == ActionType.LeaveRoom)
            {
                PlayerLeft?.Invoke(this, EventArgs.Empty);
            }
            else if (parameter.ActionType == ActionType.StartGame)
            {
                GameStarted?.Invoke(this, EventArgs.Empty);
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

        public void MakeMove(Move move)
        {
            _gameClient.Send(new MakeMoveParameter(_sessionId) { MoveId = move.Id });
        }

        public void StartGame()
        {
            _gameClient.Send(new StartGameParameter(_sessionId));
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
