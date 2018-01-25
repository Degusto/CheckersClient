using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using CheckersClient.Extensions;
using CheckersClient.Views.Controls;
using CheckersCommon.Enums;
using CheckersCommon.Models;
using CheckersCommon.Presenters.Contracts;

namespace CheckersCommon.Views
{
    public partial class MainView : Form, IMainView
    {
        private const int RowCount = 8;
        private const int ColumnCount = 8;
        private bool IsHost
        {
            get
            {
                return hostRadioButton.Checked;
            }
        }

        public event EventHandler<Move> MakeMove;
        public event EventHandler<EventArgs> LeaveGame;
        public event EventHandler<EventArgs> EnterGame;
        public event EventHandler<EventArgs> StartGame;

        public MainView()
        {
            InitializeComponent();

            hostRadioButton.Checked = true;

            GenerateGameboard();
        }

        private void GenerateGameboard()
        {
            for (int row = 0; row < RowCount; row++)
            {
                for (int column = 0; column < ColumnCount; column++)
                {
                    var cellControl = new CellControl
                    {
                        Dock = DockStyle.Fill,
                        Margin = new Padding(0),
                        PawnVisible = false
                    };

                    cellControl.BackColor = (row + column) % 2 == 0 ? Color.LightYellow : Color.SaddleBrown;
                    cellControl.MakeMove += async (sender, args) => await Task.Run(() => OnPawnMakeMove(sender, args));
                    cellControl.ReleaseMouse += async (sender, args) => await Task.Run(() => OnPawnReleaseMouse(sender, args));
                    cellControl.CaptureMouse += async (sender, args) => await Task.Run(() => OnPawnCaptureMouse(sender, args));

                    gameBoardTableLayoutPanel.Controls.Add(cellControl);
                    gameBoardTableLayoutPanel.SetCellPosition(cellControl, new TableLayoutPanelCellPosition { Row = row, Column = column });
                }
            }
        }

        public void ShowInfo(string message)
        {
            MessageBox.Show(message);
        }

        public PlayerType PlayerType
        {
            get
            {
                return IsHost ? PlayerType.Host : PlayerType.Guest;
            }
        }

        public IEnumerable<Pawn> Pawns
        {
            set
            {
                this.InvokeIfRequired(() => SetPawns(value));
            }
        }

        public string RoomId
        {
            get
            {
                return roomIdTextBox.Text;
            }
            set
            {
                this.InvokeIfRequired(() => roomIdTextBox.Text = value);
            }
        }

        public bool CanChangePlayerType
        {
            set
            {
                this.InvokeIfRequired(() => hostRadioButton.Enabled = guestRadioButton.Enabled = value);
            }
        }

        public bool CanLeaveGame
        {
            set
            {
                this.InvokeIfRequired(() => leaveGameButton.Enabled = value);
            }
        }

        public bool CanStartGame
        {
            set
            {
                this.InvokeIfRequired(() => startGameButton.Enabled = value);
            }
        }

        public bool CanEditRoomId
        {
            set
            {
                this.InvokeIfRequired(() => roomIdTextBox.ReadOnly = !value);
            }
        }

        public bool CanEnterGame
        {
            set
            {
                this.InvokeIfRequired(() =>
                {
                    enterGameButton.Enabled = value;
                    roomIdTextBox.ReadOnly = !value || PlayerType == PlayerType.Host;
                });
            }
        }

        public GameStatus GameStatus
        {
            set
            {
                this.InvokeIfRequired(() =>
                {
                    switch (value)
                    {
                        case GameStatus.HostTurn:
                            currentPlayerLabel.Text = "BIAŁY";
                            break;
                        case GameStatus.GuestTurn:
                            currentPlayerLabel.Text = "CZARNY";
                            break;
                        default:
                            currentPlayerLabel.Text = string.Empty;
                            break;
                    }
                });
            }
        }

        private void SetPawns(IEnumerable<Pawn> pawns)
        {
            hostPawnCountLabel.Text = pawns.Where(p => p.Owner == PlayerType.Host).Count().ToString();
            guestPawnCountLabel.Text = pawns.Where(p => p.Owner == PlayerType.Guest).Count().ToString();

            ResetPawns();

            foreach (var pawn in pawns)
            {
                var cellControl = (CellControl)gameBoardTableLayoutPanel.GetControlFromPosition(pawn.Position.Column, pawn.Position.Row);

                cellControl.Pawn = pawn;
                cellControl.PawnVisible = true;
            }
        }

        private void ResetPawns()
        {
            foreach (var cellControl in gameBoardTableLayoutPanel.Controls.OfType<CellControl>())
            {
                cellControl.PawnVisible = false;
                cellControl.CapturedMouse = false;
                cellControl.IsPlaceholder = false;
            }
        }

        private void OnHostRadioButtonCheckedChanged(object sender, EventArgs e)
        {
            enterGameButton.Text = IsHost ? "Załóż nowy pokój" : "Dołącz do pokoju";
            leaveGameButton.Text = IsHost ? "Zamknij pokój" : "Opuść pokój";
            roomIdTextBox.ReadOnly = IsHost;
        }

        private async void OnEnterGameButtonClick(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                if (EnterGame != null)
                {
                    EnterGame.Invoke(this, EventArgs.Empty);
                }
            });
        }

        private async void OnLeaveGameButtonClick(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                if (LeaveGame != null)
                {
                    LeaveGame.Invoke(this, EventArgs.Empty);
                }
            });
        }

        private async void OnStartGameButtonClick(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                if (StartGame != null)
                {
                    StartGame.Invoke(this, EventArgs.Empty);
                }
            });
        }

        private void OnPawnMakeMove(object sender, EventArgs e)
        {
            var pawnControl = gameBoardTableLayoutPanel.Controls.OfType<CellControl>().Single(x => x.CapturedMouse);
            var placeholderControl = (CellControl)sender;
            var position = gameBoardTableLayoutPanel.GetPositionFromControl(placeholderControl);

            var move = pawnControl.Pawn.AvailableMoves.Single(x => x.DestinatedPosition.Row == position.Row && x.DestinatedPosition.Column == position.Column);

            if (MakeMove != null)
            {
                MakeMove.Invoke(this, move);
            }
        }

        private void OnPawnReleaseMouse(object sender, EventArgs e)
        {
            HidePlaceholders();
        }

        private void OnPawnCaptureMouse(object sender, EventArgs e)
        {
            HidePlaceholders();

            var cellControl = (CellControl)sender;

            ClearCapture(cellControl);

            ShowMoves(cellControl);
        }

        private void ClearCapture(CellControl cellControl)
        {
            foreach (var cell in gameBoardTableLayoutPanel.Controls.OfType<CellControl>().Where(x => x.CapturedMouse && x != cellControl))
            {
                cell.CapturedMouse = false;
            }
        }

        private void ShowMoves(CellControl cellControl)
        {
            foreach (var move in cellControl.Pawn.AvailableMoves)
            {
                var placeholder = (CellControl)gameBoardTableLayoutPanel.GetControlFromPosition(move.DestinatedPosition.Column, move.DestinatedPosition.Row);

                placeholder.IsPlaceholder = true;
                placeholder.PawnVisible = true;
            }
        }

        private void HidePlaceholders()
        {
            foreach (var placeholder in gameBoardTableLayoutPanel.Controls.OfType<CellControl>().Where(x => x.IsPlaceholder))
            {
                placeholder.IsPlaceholder = false;
                placeholder.CapturedMouse = false;
                placeholder.PawnVisible = false;
            }
        }
    }
}