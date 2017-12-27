using System;

namespace CheckersClient.Exceptions
{
    public sealed class ServerException : Exception
    {
        public ServerException(string message) : base(message)
        {
        }
    }
}
