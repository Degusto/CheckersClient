using System;
using System.Diagnostics;

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
        event EventHandler<string> DataReceived;

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
        private readonly string Id = Guid.NewGuid().ToString();

        public GameClient(string address, int port)
        {
            _port = port;
            _address = address;

            _client = new SimpleTcpClient { Delimiter = 0 };

            _client.DataReceived += OnDataReceived;
        }

        public event EventHandler<string> DataReceived;

        private void OnDataReceived(object sender, Message message)
        {
#warning remove later
            try
            {
                string json = message.MessageString;

                Parameter parameter = JsonConvert.DeserializeObject<Parameter>(json);

                DataReceived?.Invoke(sender, json);
            }
            catch (Exception ex)
            {
                if (!Debugger.IsAttached)
                {
                    System.Windows.Forms.MessageBox.Show(Id + ex.Message);
                }

                throw;
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

            string parameterJson = JsonConvert.SerializeObject(data);

            string resultJson = SendRequest(parameterJson);

            return JsonConvert.DeserializeObject<TResult>(resultJson);
        }

        public void Send<TParameter>(TParameter data)
            where TParameter : Parameter
        {
            data.NotNull();

            string json = JsonConvert.SerializeObject(data);

            SendRequest(json);
        }

        private string SendRequest(string parameterJson)
        {
            if (_client.TcpClient == null || !_client.TcpClient.Connected)
            {
                _client.Connect(_address, _port);
            }

            string resultJson = _client.WriteLineAndGetReply(parameterJson, TimeSpan.FromHours(1)).MessageString;

            Result result = JsonConvert.DeserializeObject<Result>(resultJson);

            if (!result.Success)
            {
                if (result.Error == "player_disconnected")
                {
                    throw new PlayerDisconnectedException();
                }
                else
                {
                    throw new ServerException(result.Error);
                }
            }

            return resultJson;
        }
    }
}
