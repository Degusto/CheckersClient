using System;

namespace CheckersTestServer
{
    internal sealed class Program
    {
        private static void Main()
        {
            var server = new Server(new Logger());

            server.Start();

            Console.ReadLine();

            server.Stop();
        }
    }
}