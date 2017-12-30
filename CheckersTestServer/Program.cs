using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using CheckersCommon;
using CheckersCommon.Enums;
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
                Parameter parameter = JsonConvert.DeserializeObject<Parameter>(e.MessageString);

                Result result = GetResult(parameter.ActionType, e.MessageString, e.TcpClient);

                string json = JsonConvert.SerializeObject(result);

                Console.WriteLine($"Action type: {parameter.ActionType}({(int)parameter.ActionType})");
                Console.WriteLine($"Parameter: {e.MessageString}");
                Console.WriteLine($"Result: {json}");
                Console.WriteLine();

                e.Reply(json);
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
                    return CreateRoom(json, client);
                case ActionType.StartGame:
                    return StartGame(json);
                case ActionType.CloseRoom:
                    return CloseRoom(json);
                case ActionType.JoinRoom:
                    return JoinRoom(json, client);
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

        private static Result CreateRoom(string json, TcpClient client)
        {
            var parameter = JsonConvert.DeserializeObject<CreateRoomParameter>(json);

            var game = new Game { Host = client, HostSessionId = Guid.NewGuid().ToString(), RoomId = Guid.NewGuid().ToString() };

            Games.Add(game);

            return new CreateRoomResult { SessionId = game.HostSessionId, RoomId = game.RoomId };
        }

        private static Result StartGame(string json)
        {
            var parameter = JsonConvert.DeserializeObject<NewGameParameter>(json);

            var game = Games.FirstOrDefault(g => g.HostSessionId == parameter.SessionId);

            if (game == null)
            {
                return Result.CreateError("Couldn't find room");
            }

            if(game.Guest == null)
            {
                return Result.CreateError("You can't start game without second player");
            }

            game.GameInProgress = true;
            game.Guest.Client.Send(Encoding.UTF8.GetBytes(json));

            return Result.CreateSuccess();
        }

        private static Result CloseRoom(string json)
        {
            var parameter = JsonConvert.DeserializeObject<CloseRoomParameter>(json);

            var game = Games.FirstOrDefault(g => g.HostSessionId == parameter.SessionId);

            if (game == null)
            {
                return Result.CreateError("Couldn't find active room");
            }

            if (game.GameInProgress)
            {
                return Result.CreateError("Game is in progress");
            }

            Games.Remove(game);

            return Result.CreateSuccess();
        }

        private static Result JoinRoom(string json, TcpClient client)
        {
            var parameter = JsonConvert.DeserializeObject<JoinRoomParameter>(json);

            var game = Games.FirstOrDefault(p => p.RoomId == parameter.RoomId);

            if (game == null)
            {
                return Result.CreateError("Couldn't find active room");
            }

            if (game.GuestSessionId != null)
            {
                return Result.CreateError("Room is full");
            }

            game.Guest = client;
            game.GuestSessionId = Guid.NewGuid().ToString();
            game.Host.Client.Send(Encoding.UTF8.GetBytes(json));

            return new JoinRoomResult { SessionId = game.GuestSessionId };
        }

        private static Result LeaveRoom(string json)
        {
            var parameter = JsonConvert.DeserializeObject<LeaveRoomParameter>(json);

            var game = Games.FirstOrDefault(p => p.GuestSessionId == parameter.SessionId);

            if (game == null)
            {
                return Result.CreateError("Couldn't find active room");
            }

            if (game.GameInProgress)
            {
                return Result.CreateError("Game is in progress");
            }

            game.Host.Client.Send(Encoding.UTF8.GetBytes(json));
            game.Guest = null;
            game.GuestSessionId = null;

            return Result.CreateSuccess();
        }

        private static Result Surrender(string json)
        {
            var parameter = JsonConvert.DeserializeObject<SurrenderParameter>(json);

            var game = Games.FirstOrDefault(p => p.GuestSessionId == parameter.SessionId || p.HostSessionId == parameter.SessionId);

            if (game == null)
            {
                return Result.CreateError("Couldn't find active room");
            }

            if(game.GuestSessionId == parameter.SessionId)
            {
                game.Host.Client.Send(Encoding.UTF8.GetBytes(json));
            }
            else
            {
                game.Guest.Client.Send(Encoding.UTF8.GetBytes(json));
            }

            game.GameInProgress = false;

            return Result.CreateSuccess();
        }
    }
}
