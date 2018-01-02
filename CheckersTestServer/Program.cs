using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using CheckersCommon;
using CheckersCommon.Enums;
using CheckersCommon.Extensions;
using CheckersCommon.Parameters;
using CheckersCommon.Results;

using Newtonsoft.Json;

using SimpleTCP;

namespace CheckersTestServer
{
    internal sealed class Program
    {
        private static IList<Game> Games = new List<Game>();

        static void Main(string[] args)
        {
            var server = new SimpleTcpServer();

            server.DataReceived += OnDataReceived;
            server.Start(12345);

            Console.ReadLine();
        }

        private static void OnDataReceived(object sender, Message e)
        {
            try
            {
                string parameterJson = e.MessageString;

                Parameter parameter = JsonConvert.DeserializeObject<Parameter>(parameterJson);

                Result result = GetResult(parameter.ActionType, parameterJson, e.TcpClient);

                string resultJson = JsonConvert.SerializeObject(result);

                Console.WriteLine($"Action type: {parameter.ActionType}({(int)parameter.ActionType})");
                Console.WriteLine($"Parameter: {parameterJson}");
                Console.WriteLine($"Result: {resultJson}");
                Console.WriteLine();

                Thread.Sleep(100);

                e.Reply(resultJson);
            }
            catch (Exception ex)
            {
                e.Reply(JsonConvert.SerializeObject(Result.CreateError(ex.Message)));
            }
        }

        private static Result GetResult(ActionType actionType, string json, TcpClient client)
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
                    return null;
                case ActionType.UpdateGameboard:
                    return null;
                default:
                    throw new ArgumentOutOfRangeException("Unknown action type");
            }
        }

        private static void UpdateGameboard(Game game)
        {
            UpdateGameboardParameter result = new UpdateGameboardParameter
            {
                Pawns = game.Host.Pawns.Concat(game.Guest.Pawns),
                StartDate = game.StartDate,
                GameStatus = game.Status
            };

            string json = JsonConvert.SerializeObject(result);

            var host = game.Host.Client;
            var guest = game.Guest.Client;

            if (host != null)
            {
                host.Send(json);
            }

            if (guest != null)
            {
                guest.Send(json);
            }

            Console.WriteLine(json);
        }

        private static Result CreateRoom(string json, Socket client)
        {
            var parameter = JsonConvert.DeserializeObject<CreateRoomParameter>(json);

            var game = new Game { Host = new Player { Client = client, SessionId = Guid.NewGuid().ToString() }, RoomId = Guid.NewGuid().ToString() };

            game.UpdateGameboard += (sender, _) => UpdateGameboard((Game)sender);

            Games.Add(game);

            return new CreateRoomResult { SessionId = game.Host.SessionId, RoomId = game.RoomId };
        }

        private static Result StartGame(string json)
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
            game.Guest.Client.Send(json);

            return Result.CreateSuccess();
        }

        private static Result CloseRoom(string json)
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

        private static Result JoinRoom(string json, Socket client)
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

            game.Guest.Client = client;
            game.Guest.SessionId = Guid.NewGuid().ToString();
            game.Host.Client.Send(json);

            return new JoinRoomResult { SessionId = game.Guest.SessionId };
        }

        private static Result LeaveRoom(string json)
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

            game.Host.Client.Send(json);
            game.Guest.Client = null;
            game.Guest.SessionId = null;

            return Result.CreateSuccess();
        }

        private static Result Surrender(string json)
        {
            var parameter = JsonConvert.DeserializeObject<SurrenderParameter>(json);

            var game = Games.FirstOrDefault(p => p.Guest.SessionId == parameter.SessionId || p.Host.SessionId == parameter.SessionId);

            if (game == null)
            {
                return Result.CreateError("Couldn't find active room");
            }

            if (game.Guest.SessionId == parameter.SessionId)
            {
                game.Surrender(PlayerType.Guest);
            }
            else
            {
                game.Surrender(PlayerType.Host);
            }

            return Result.CreateSuccess();
        }
    }
}
