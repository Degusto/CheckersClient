using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

using CheckersCommon.Enums;
using CheckersCommon.Models;

namespace CheckersTestServer
{
    internal sealed class Game
    {
        private GameStatus _gameStatus;

        private readonly Timer _timer = new Timer(3000);

        internal Game()
        {
            Host = new Player();
            Guest = new Player();

            _timer.Elapsed += (_, __) => UpdateGameboard?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler<EventArgs> UpdateGameboard;

        internal DateTime StartDate { get; private set; }

        internal Player Host { get; set; }

        internal Player Guest { get; set; }

        internal string RoomId { get; set; }

        internal GameStatus Status
        {
            get => _gameStatus;
            set
            {
                _gameStatus = value;

                if(InProgress)
                {
                    _timer.Start();
                }
                else
                {
                    _timer.Stop();
                }

                UpdateGameboard?.Invoke(this, EventArgs.Empty);
            }
        }

        internal void StartGame()
        {
            StartDate = DateTime.Now;
            Host.Pawns = GetHostPawns().ToList();
            Guest.Pawns = GetGuestPawns().ToList();

            Status = GameStatus.HostTurn;
        }

        private IEnumerable<Pawn> GetHostPawns()
        {
            for(int i = 0; i < 3; i++)
            {
                for(int j = i % 2 == 0 ? 1 : 0; j < 8; j += 2)
                {
                    yield return new Pawn { Owner = PlayerType.Host, Position = (i, j) };
                }
            }
        }

        private IEnumerable<Pawn> GetGuestPawns()
        {
            for (int i = 5; i < 8; i++)
            {
                for (int j = i % 2 == 0 ? 0 : 1; j < 8; j += 2)
                {
                    yield return new Pawn { Owner = PlayerType.Guest, Position = (i, j) };
                }
            }
        }

        internal void Surrender(PlayerType player)
        {
            Status = player == PlayerType.Host ? GameStatus.GuestWithdrew : GameStatus.HostWithdrew;
        }

        public bool InProgress => Status == GameStatus.HostTurn || Status == GameStatus.GuestTurn;
    }
}
