using System;

using CheckersClient.Exceptions;
using CheckersCommon.Parameters;
using CheckersCommon.Results;
using CheckersCommon.Utilities;

using Newtonsoft.Json;

using SimpleTCP;

namespace CheckersCommon.Models
{
    internal interface IGameClient
    {
        event EventHandler<Message> DataReceived;

        void Send<TParameter>(TParameter data)
            where TParameter : Parameter;

        TResult Get<TParameter, TResult>(TParameter data)
            where TParameter : Parameter
            where TResult : Result;
    }

    internal sealed class GameClient : IGameClient, IDisposable
    {
        private readonly int _port;
        private readonly string _address;
        private readonly SimpleTcpClient _client;

        public GameClient(string address, int port)
        {
            _port = port;
            _address = address;

            _client = new SimpleTcpClient
            {
                Delimiter = 0
            };

            _client.DataReceived += OnDataReceived;
        }

        public event EventHandler<Message> DataReceived;

        private void OnDataReceived(object sender, Message message)
        {
            Parameter parameter = JsonConvert.DeserializeObject<Parameter>(message.MessageString);

            if (parameter.ActionType != ActionType.KeepAlive)
            {
                DataReceived?.Invoke(sender, message);
            }
        }

        public void Dispose()
        {
            _client.DataReceived -= OnDataReceived;
            _client.Dispose();
        }

        public TResult Get<TParameter, TResult>(TParameter data)
            where TParameter : Parameter
            where TResult : Result
        {
            data.NotNull();

            string json = JsonConvert.SerializeObject(data);

            Message message = SendRequest(json);

            return JsonConvert.DeserializeObject<TResult>(message.MessageString);
        }

        public void Send<TParameter>(TParameter data)
            where TParameter : Parameter
        {
            data.NotNull();

            string json = JsonConvert.SerializeObject(data);

            SendRequest(json);
        }

        private Message SendRequest(string json)
        {
            if (_client.TcpClient == null || !_client.TcpClient.Connected)
            {
                _client.Connect(_address, _port);
            }

            Message message = _client.WriteLineAndGetReply(json, TimeSpan.FromHours(1));

            Result result = JsonConvert.DeserializeObject<Result>(message.MessageString);

            if (!result.Success)
            {
                throw new ServerException(result.Error);
            }

            return message;
        }
    }
}
