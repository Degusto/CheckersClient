using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using CheckersClient.Exceptions;
using CheckersCommon.Parameters;
using CheckersCommon.Results;
using CheckersCommon.Utilities;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SimpleTCP;

using Message = SimpleTCP.Message;

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
        private const char Delimiter = '|';

        private readonly int _port;
        private readonly string _address;
        private readonly SimpleTcpClient _client;

        public GameClient(string address, int port)
        {
            _port = port;
            _address = address;

            _client = new SimpleTcpClient { Delimiter = (byte)Delimiter };
            _client.DelimiterDataReceived += OnDelimiterDataReceived;
        }

        public event EventHandler<string> DataReceived;

        private string firstPart = string.Empty;
        private void OnDelimiterDataReceived(object sender, Message message)
        {
            string json = firstPart + message.MessageString;

            try
            {
                if (DataReceived != null)
                {
                    DataReceived.Invoke(sender, json);
                }

                firstPart = string.Empty;
            }
            catch (JsonReaderException ex)
            {
                firstPart += json;
                System.Windows.Forms.MessageBox.Show(firstPart);
            }
        }

        public void Dispose()
        {
            _client.DataReceived -= OnDelimiterDataReceived;
            _client.Dispose();
        }

        public TResult Get<TParameter, TResult>(TParameter data)
            where TParameter : Parameter
            where TResult : Result
        {
            data.NotNull();

            string parameterJson = Serialize(data);

            string resultJson = SendRequest(parameterJson);

            return Deserialize<TResult>(resultJson);
        }

        public void Send<TParameter>(TParameter data)
            where TParameter : Parameter
        {
            data.NotNull();

            string json = Serialize(data);

            SendRequest(json);
        }

        private string SendRequest(string parameterJson)
        {
            if (_client.TcpClient == null || !_client.TcpClient.Connected)
            {
                _client.Connect(_address, _port);
                _client.TcpClient.ReceiveBufferSize = int.MaxValue;
                _client.TcpClient.SendBufferSize = int.MaxValue;
                _client.TcpClient.NoDelay = true;
            }

            Message reply = null;
            EventHandler<Message> action = (_, m) => reply = IsResult(m.MessageString) ? m : null;

            _client.DelimiterDataReceived += action;
            _client.Write(parameterJson + Delimiter);

            while (reply == null)
            {
                Thread.Sleep(100);
            }

            _client.DelimiterDataReceived -= action;

            string resultJson = reply.MessageString;
            //string resultJson = _client.WriteLineAndGetReply(parameterJson + Delimiter, TimeSpan.FromHours(1)).MessageString;

            //resultJson = resultJson.Split('|').Where(x => !string.IsNullOrWhiteSpace(x)).First(IsResult);

            Result result = Deserialize<Result>(resultJson);

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

        private bool IsResult(string json)
        {
            try
            {
                Deserialize<Result>(json);

                return true;
            }
            catch (JsonReaderException)
            {
                return false;
            }
        }

        private string Serialize(object @object)
        {
            var settings = new JsonSerializerSettings() { ContractResolver = new NullToEmptyStringResolver() };

            return JsonConvert.SerializeObject(@object, settings);
        }

        private TObject Deserialize<TObject>(string json)
        {
            try
            {
                var settings = new JsonSerializerSettings() { ContractResolver = new NullToEmptyStringResolver() };

                return JsonConvert.DeserializeObject<TObject>(json.Trim(), settings);
            }
            catch (Exception)
            {
                MessageBox.Show("SEND REQUEST: " + json);
                Debugger.Launch();
                throw;
            }
        }

        public class NullToEmptyStringResolver : DefaultContractResolver
        {
            protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
            {
                return type.GetProperties()
                        .Select(p =>
                        {
                            var jp = base.CreateProperty(p, memberSerialization);
                            jp.ValueProvider = new NullToEmptyStringValueProvider(p);
                            return jp;
                        }).ToList();
            }
        }

        public class NullToEmptyStringValueProvider : IValueProvider
        {
            PropertyInfo _MemberInfo;
            public NullToEmptyStringValueProvider(PropertyInfo memberInfo)
            {
                _MemberInfo = memberInfo;
            }

            public object GetValue(object target)
            {
                object result = _MemberInfo.GetValue(target);
                if (_MemberInfo.PropertyType == typeof(string) && result == null) result = "";
                return result;

            }

            public void SetValue(object target, object value)
            {
                _MemberInfo.SetValue(target, (value as string) != string.Empty ? value : null);
            }
        }
    }
}
