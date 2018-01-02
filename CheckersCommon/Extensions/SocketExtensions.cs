using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace CheckersCommon.Extensions
{
    public static class SocketExtensions
    {
        public static void Send(this Socket client, string json)
        {
            Thread.Sleep(100);

            byte[] message = Encoding.UTF8.GetBytes(json);

            client.Send(message);
        }
    }
}
