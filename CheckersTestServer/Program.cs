using System;
using CheckersCommon;
using CheckersCommon.Parameters;
using CheckersCommon.Results;
using Newtonsoft.Json;
using SimpleTCP;

namespace CheckersTestServer
{
    class Program
    {
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

                Result result = GetResult(parameter.ActionType);

                e.Reply(JsonConvert.SerializeObject(result));
            }
            catch (Exception ex)
            {
                e.Reply(JsonConvert.SerializeObject(Result.CreateError(ex.Message)));
            }
        }

        private static Result GetResult(ActionType actionType)
        {
            switch (actionType)
            {
                case ActionType.NewRoom:
                    return new NewRoomResult { SessionId = Guid.NewGuid().ToString(), RoomId = Guid.NewGuid().ToString() };
                case ActionType.NewGame:
                    return null;
                case ActionType.CloseRoom:
                    return null;
                case ActionType.JoinRoom:
                    return null;
                case ActionType.LeaveRoom:
                    return null;
                case ActionType.OfferDraw:
                    return null;
                case ActionType.Surrender:
                    return null;
                case ActionType.MakeMove:
                    return null;
                case ActionType.UpdateGameboard:
                    return null;
                case ActionType.DrawRequest:
                    return null;
                default:
                    throw new ArgumentOutOfRangeException("Unknown action type");
            }
        }
    }
}
