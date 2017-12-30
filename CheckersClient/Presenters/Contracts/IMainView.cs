﻿using System;
using CheckersCommon.Enums;

namespace CheckersCommon.Presenters.Contracts
{
    internal interface IMainView
    {
        PlayerType PlayerType { get; }

        string RoomId { get; set; }

        bool CanEnterGame { set; }
        bool CanLeaveGame { set; }
        bool CanSurrender { set; }
        bool CanStartGame { set; }
        bool CanChangePlayerType { set; }

        event EventHandler<EventArgs> Surrender;
        event EventHandler<EventArgs> LeaveGame;
        event EventHandler<EventArgs> EnterGame;
        event EventHandler<EventArgs> StartGame;

        void ShowInfo(string message);
    }
}