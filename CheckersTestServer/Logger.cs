using System;

namespace CheckersTestServer
{
    internal interface ILogger
    {
        void Log(string message);
    }

    internal sealed class Logger : ILogger
    {
        public void Log(string message) => Console.WriteLine(message);
    }
}
