using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using CheckersCommon;
using CheckersCommon.Enums;
using CheckersCommon.Models;
using CheckersCommon.Parameters;
using CheckersCommon.Results;

using Newtonsoft.Json;

using SimpleTCP;

namespace CheckersTestServer
{
    internal sealed class Server
    {
        private readonly ILogger _logger;
        private readonly SimpleTcpServer _server;
        private readonly IList<Game> Games = new List<Game>();

        public Server(ILogger logger)
        {
            _logger = logger;
            _server = new SimpleTcpServer();
            _server.DataReceived += OnDataReceived;
        }

        internal void Start() => _server.Start(12345);

        internal void Stop() => _server.Stop();

        private void Log(string message) => _logger.Log(message);

        private void OnDataReceived(object sender, Message e)
        {
            try
            {
                string parameterJson = e.MessageString;

                Parameter parameter = JsonConvert.DeserializeObject<Parameter>(parameterJson);

                Result result = GetResult(parameter.ActionType, parameterJson, e.TcpClient);

                string resultJson = JsonConvert.SerializeObject(result);

                Log($"Action type: {parameter.ActionType}({(int)parameter.ActionType})");
                Log($"Parameter: {parameterJson}");
                Log($"Result: {resultJson}");
                Log(string.Empty);

                Thread.Sleep(100);

                e.Reply(resultJson);
            }
            catch (SocketException)
            {
                e.Reply(JsonConvert.SerializeObject(Result.CreateError("player_disconnected")));
            }
            catch (Exception ex)
            {
                e.Reply(JsonConvert.SerializeObject(Result.CreateError(ex.Message)));
            }
        }

        private Result GetResult(ActionType actionType, string json, TcpClient client)
        {
            switch (actionType)
            {
                case ActionType.CreateRoom:
                    return CreateRoom(json, client.Client);
                case ActionType.StartGame:
                    return StartGame(json);
                case ActionType.CloseRoom:
                    return CloseRoom(json);
                case ActionType.JoinRoom:
                    return JoinRoom(json, client.Client);
                case ActionType.LeaveRoom:
                    return LeaveRoom(json);
                case ActionType.Surrender:
                    return Surrender(json);
                case ActionType.MakeMove:
                    return MakeMove(json);
                case ActionType.UpdateGameboard:
                    return Result.CreateError("Action can't be executed by client");
                default:
                    throw new ArgumentOutOfRangeException("Unknown action type");
            }
        }

        private Result CreateRoom(string json, Socket host)
        {
            var parameter = JsonConvert.DeserializeObject<CreateRoomParameter>(json);

            var game = Game.Create(host);

            game.UpdateGameboard += (sender, _) => UpdateGameboard((Game)sender);

            Games.Add(game);

            return new CreateRoomResult { SessionId = game.Host.SessionId, RoomId = game.RoomId };
        }

        private Result CloseRoom(string json)
        {
            var parameter = JsonConvert.DeserializeObject<CloseRoomParameter>(json);

            var game = Games.FirstOrDefault(g => g.Host.SessionId == parameter.SessionId);

            if (game == null)
            {
                return Result.CreateError("Couldn't find active room");
            }

            if (game.InProgress)
            {
                return Result.CreateError("Game is in progress");
            }

            Games.Remove(game);

            return Result.CreateSuccess();
        }

        private Result JoinRoom(string json, Socket client)
        {
            var parameter = JsonConvert.DeserializeObject<JoinRoomParameter>(json);

            var game = Games.FirstOrDefault(p => p.RoomId == parameter.RoomId);

            if (game == null)
            {
                return Result.CreateError("Couldn't find active room");
            }

            if (game.Guest.SessionId != null)
            {
                return Result.CreateError("Room is full");
            }

            game.AddGuest(client);
            game.Host.SendMessage(json);

            return new JoinRoomResult { SessionId = game.Guest.SessionId };
        }

        private Result LeaveRoom(string json)
        {
            var parameter = JsonConvert.DeserializeObject<LeaveRoomParameter>(json);

            var game = Games.FirstOrDefault(p => p.Guest.SessionId == parameter.SessionId);

            if (game == null)
            {
                return Result.CreateError("Couldn't find active room");
            }

            if (game.InProgress)
            {
                return Result.CreateError("Game is in progress");
            }

            game.Host.SendMessage(json);
            game.RemoveGuest();

            return Result.CreateSuccess();
        }

        private Result StartGame(string json)
        {
            var parameter = JsonConvert.DeserializeObject<StartGameParameter>(json);

            var game = Games.FirstOrDefault(g => g.Host.SessionId == parameter.SessionId);

            if (game == null)
            {
                return Result.CreateError("Couldn't find room");
            }

            if (game.Guest == null)
            {
                return Result.CreateError("You can't start game without second player");
            }

            game.StartGame();
            game.Guest.SendMessage(json);

            return Result.CreateSuccess();
        }

        private Result Surrender(string json)
        {
            var parameter = JsonConvert.DeserializeObject<SurrenderParameter>(json);

            var game = Games.FirstOrDefault(p => p.Guest.SessionId == parameter.SessionId || p.Host.SessionId == parameter.SessionId);

            if (game == null)
            {
                return Result.CreateError("Couldn't find active room");
            }

            var playerType = game.Host.SessionId == parameter.SessionId ? PlayerType.Host : PlayerType.Guest;

            game.Surrender(playerType);

            return Result.CreateSuccess();
        }

        private Result MakeMove(string json)
        {
            var parameter = JsonConvert.DeserializeObject<MakeMoveParameter>(json);

            var game = Games.FirstOrDefault(p => p.Guest.SessionId == parameter.SessionId || p.Host.SessionId == parameter.SessionId);

            game.MakeMove(parameter.MoveId);

            UpdateGameboard(game);

            return Result.CreateSuccess();
        }

        private void UpdateGameboard(Game game)
        {
            var hostResult = GetUpdateGameboardParameter(game, PlayerType.Host);
            var guestResult = GetUpdateGameboardParameter(game, PlayerType.Guest);

            string hostJson = JsonConvert.SerializeObject(hostResult);
            string guestJSon = JsonConvert.SerializeObject(guestResult);

            SendUpdateGameboardMessage(game.Host, hostJson);
            SendUpdateGameboardMessage(game.Guest, guestJSon);
        }

        private UpdateGameboardParameter GetUpdateGameboardParameter(Game game, PlayerType playerType)
        {
            return new UpdateGameboardParameter
            {
                Pawns = GetPawns(game, playerType),
                GameStatus = game.Status,
            };
        }

        private void SendUpdateGameboardMessage(Player player, string json)
        {
            if (player != null)
            {
                player.SendMessage(json);

                Log(json);
            }
        }

        private static IEnumerable<Pawn> GetPawns(Game game, PlayerType playerType)
        {
            if (game.Status == GameStatus.HostTurn && playerType == PlayerType.Host)
            {
                return game.HostPawns.Concat(game.GuestPawns.Select(p => new Pawn { Position = p.Position, IsPromoted = p.IsPromoted, Owner = p.Owner }));
            }
            else if (game.Status == GameStatus.HostTurn && playerType == PlayerType.Guest)
            {
                return game.HostPawns.Concat(game.GuestPawns).Select(p => new Pawn { Position = p.Position, IsPromoted = p.IsPromoted, Owner = p.Owner });
            }
            else if (game.Status == GameStatus.GuestTurn && playerType == PlayerType.Host)
            {
                return game.GuestPawns.Concat(game.HostPawns).Select(p => new Pawn { Position = p.Position, IsPromoted = p.IsPromoted, Owner = p.Owner });
            }
            else
            {
                return game.GuestPawns.Concat(game.HostPawns.Select(p => new Pawn { Position = p.Position, IsPromoted = p.IsPromoted, Owner = p.Owner }));
            }
        }
    }
}
