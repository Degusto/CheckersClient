using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;

using CheckersCommon.Enums;
using CheckersCommon.Models;

namespace CheckersTestServer
{
    internal sealed class Game
    {
        private const int RowCount = 8;
        private const int ColumnCount = 8;

        private GameStatus _gameStatus;

        private readonly List<Pawn> _pawns = new List<Pawn>();

        public event EventHandler<EventArgs> UpdateGameboard;

        private Game() { }

        internal DateTime StartDate { get; private set; }

        internal Player Host { get; private set; }

        internal Player Guest { get; private set; }

        internal string RoomId { get; private set; }

        internal IEnumerable<Pawn> HostPawns => _pawns.Where(p => p.Owner == PlayerType.Host).ToList();

        internal IEnumerable<Pawn> GuestPawns => _pawns.Where(p => p.Owner == PlayerType.Guest).ToList();

        internal bool InProgress => Status == GameStatus.HostTurn || Status == GameStatus.GuestTurn;

        internal GameStatus Status
        {
            get => _gameStatus;
            set
            {
                _gameStatus = value;

                UpdateGameboard?.Invoke(this, EventArgs.Empty);
            }
        }

        internal static Game Create(Socket host) => new Game
        {
            RoomId = Guid.NewGuid().ToString(),
            Guest = new Player(),
            Host = new Player(Guid.NewGuid().ToString(), host),
        };

        internal void AddGuest(Socket client) => Guest = new Player(Guid.NewGuid().ToString(), client);

        internal void RemoveGuest() => Guest = new Player();

        internal void Surrender(PlayerType player)
        {
            if (!InProgress)
            {
                throw new InvalidOperationException("Game isn't in progress");
            }

            Status = player == PlayerType.Host ? GameStatus.GuestWithdrew : GameStatus.HostWithdrew;
        }

        internal void StartGame()
        {
            if (InProgress)
            {
                throw new InvalidOperationException("Game is already in progress");
            }

            StartDate = DateTime.Now;

            _pawns.Clear();
            _pawns.AddRange(GetHostPawns().ToList());
            _pawns.AddRange(GetGuestPawns().ToList());

            AddMoves();

            Status = GameStatus.HostTurn;
        }

        private IEnumerable<Pawn> GetHostPawns()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = i % 2 == 0 ? 1 : 0; j < ColumnCount; j += 2)
                {
                    yield return new Pawn { Owner = PlayerType.Host, Position = (i, j), AvailableMoves = new List<Move>() };
                }
            }
        }

        private IEnumerable<Pawn> GetGuestPawns()
        {
            for (int i = 5; i < 8; i++)
            {
                for (int j = i % 2 == 0 ? 1 : 0; j < ColumnCount; j += 2)
                {
                    yield return new Pawn { Owner = PlayerType.Guest, Position = (i, j), AvailableMoves = new List<Move>() };
                }
            }
        }

        public void MakeMove(string moveId)
        {
            if (!InProgress)
            {
                throw new InvalidOperationException("Game isn't in progress");
            }

            MakeMove(moveId, Status == GameStatus.HostTurn ? HostPawns : GuestPawns);
        }

        private void MakeMove(string moveId, IEnumerable<Pawn> pawns)
        {
            Pawn pawn = pawns.Single(x => x.AvailableMoves.Any(m => m.Id == moveId));
            Move move = pawn.AvailableMoves.Single(x => x.Id == moveId);

            if (move.IsCapture)
            {
                _pawns.Remove(_pawns.Single(p => p.Position.Row == move.MiddlePosition.Row && p.Position.Column == move.MiddlePosition.Column));
            }

            pawn.Position = move.DestinatedPosition;
            pawn.IsPromoted = pawn.IsPromoted || IsInPromotedPosition(pawn); 

            AddMoves();

            UpdateGameStatus();
        }

        private bool IsInPromotedPosition(Pawn pawn)
        {
            const int HostKingRowIndex = 7;
            const int GuestKingRowIndex = 0;

            return pawn.Owner == PlayerType.Host ? pawn.Position.Row == HostKingRowIndex : pawn.Position.Row == GuestKingRowIndex;
        }

        private void UpdateGameStatus()
        {
            if (Status == GameStatus.HostTurn)
            {
                bool canMakeMove = GuestPawns.Any(x => x.AvailableMoves.Any());

                Status = canMakeMove ? GameStatus.GuestTurn : GameStatus.HostWon;
            }
            else if (Status == GameStatus.GuestTurn)
            {
                bool canMakeMove = HostPawns.Any(x => x.AvailableMoves.Any());

                Status = canMakeMove ? GameStatus.HostTurn : GameStatus.GuestWon;
            }

            if (!InProgress)
            {
                _pawns.ForEach(p => p.AvailableMoves.Clear());
            }
        }

        private void AddMoves()
        {
            foreach (var pawn in _pawns)
            {
                pawn.AvailableMoves.Clear();

                var position = pawn.Position;

                Position leftTop = (position.Row - 1, position.Column - 1);
                Position rightTop = (position.Row - 1, position.Column + 1);
                Position leftDown = (position.Row + 1, position.Column - 1);
                Position rightDown = (position.Row + 1, position.Column + 1);

                if (pawn.IsPromoted || pawn.Owner == PlayerType.Host)
                {
                    AddMove(leftDown, pawn);
                    AddMove(rightDown, pawn);
                }

                if (pawn.IsPromoted || pawn.Owner == PlayerType.Guest)
                {
                    AddMove(leftTop, pawn);
                    AddMove(rightTop, pawn);
                }
            }
        }

        private void AddMove(Position position, Pawn pawn, bool lookForCapture = true)
        {
            bool isValidPosition = position.Row >= 0 && position.Row <= RowCount - 1 && position.Column >= 0 && position.Column <= ColumnCount - 1;
            bool isAvailable = !_pawns.Any(p => p.Position.Row == position.Row && p.Position.Column == position.Column);
            bool isOccupiedByEnemy = isValidPosition && !isAvailable && _pawns.Single(p => p.Position.Row == position.Row && p.Position.Column == position.Column).Owner != pawn.Owner;

            if (isValidPosition && isAvailable)
            {
                pawn.AvailableMoves.Add(new Move { Id = Guid.NewGuid().ToString(), DestinatedPosition = position, SourcePosition = pawn.Position });
            }
            else if (!isAvailable && isOccupiedByEnemy && lookForCapture)
            {
                position.Row += position.Row - pawn.Position.Row;
                position.Column += position.Column - pawn.Position.Column;

                AddMove(position, pawn, lookForCapture: false);
            }
        }
    }
}
