using System;

namespace CheckersClient.Models
{
    internal interface IServer
    {
        void NewRoom();
        void NewGame();
        void CloseRoom();
        void JoinRoom();
        void LeaveRoom();
        void OfferDraw();
        void Surrender();
        void MakeMove();

        event EventHandler<EventArgs> UpdateGameboard;
        event EventHandler<EventArgs> DrawRequest;
    }

    internal sealed class Server : IServer
    {
    }
}
