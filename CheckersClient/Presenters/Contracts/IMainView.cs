﻿using System;
using System.Collections.Generic;
using CheckersCommon.Enums;
using CheckersCommon.Models;

namespace CheckersCommon.Presenters.Contracts
{
    internal interface IMainView
    {
        PlayerType PlayerType { get; }

        string RoomId { get; set; }

        bool CanEnterGame { set; }
        bool CanLeaveGame { set; }
        bool CanStartGame { set; }
        bool CanChangePlayerType { set; }

        IEnumerable<Pawn> Pawns {  set; }
        GameStatus GameStatus { set; }

        event EventHandler<Move> MakeMove;
        event EventHandler<EventArgs> LeaveGame;
        event EventHandler<EventArgs> EnterGame;
        event EventHandler<EventArgs> StartGame;

        void ShowInfo(string message);
    }
}
