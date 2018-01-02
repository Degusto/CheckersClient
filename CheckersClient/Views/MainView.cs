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
        private bool IsHost => hostRadioButton.Checked;

        public event EventHandler<EventArgs> Surrender;
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
                    var panel = new Panel
                    {
                        Dock = DockStyle.Fill,
                        Margin = new Padding(0),
                        BackColor = (row + column) % 2 == 0 ? Color.LightYellow : Color.SaddleBrown
                    };

                    panel.Controls.Add(new PawnControl { Visible = false, Dock = DockStyle.Fill });

                    gameBoardTableLayoutPanel.Controls.Add(panel);
                    gameBoardTableLayoutPanel.SetCellPosition(panel, new TableLayoutPanelCellPosition { Row = row, Column = column });
                }
            }
        }

        public void ShowInfo(string message) => MessageBox.Show(message);

        public DateTime StartDate { private get; set; }

        public PlayerType PlayerType => IsHost ? PlayerType.Host : PlayerType.Guest;

        public IEnumerable<Pawn> Pawns { set => this.InvokeIfRequired(() => SetPawns(value)); }

        public string RoomId { get => roomIdTextBox.Text; set => this.InvokeIfRequired(() => roomIdTextBox.Text = value); }

        public bool CanChangePlayerType { set => this.InvokeIfRequired(() => hostRadioButton.Enabled = guestRadioButton.Enabled = value); }

        public bool CanLeaveGame { set => this.InvokeIfRequired(() => leaveGameButton.Enabled = value); }

        public bool CanSurrender { set => this.InvokeIfRequired(() => surrenderButton.Enabled = value); }

        public bool CanStartGame { set => this.InvokeIfRequired(() => startGameButton.Enabled = value); }

        public bool CanEditRoomId { set => this.InvokeIfRequired(() => roomIdTextBox.ReadOnly = !value); }

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
                    startDateTimer.Enabled = value == GameStatus.HostTurn || value == GameStatus.GuestTurn;

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

            foreach (var pawn in gameBoardTableLayoutPanel.Controls.OfType<Panel>().Select(p => p.Controls.OfType<PawnControl>().Single()))
            {
                pawn.Visible = false;
            }

            foreach (var pawn in pawns)
            {
                var panel = (Panel)gameBoardTableLayoutPanel.GetControlFromPosition(pawn.Position.Column, pawn.Position.Row);

                var pawnControl = panel.Controls.OfType<PawnControl>().Single();

                pawnControl.PlayerType = pawn.Owner;
                pawnControl.Visible = true;
            }
        }

        private void hostRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            enterGameButton.Text = IsHost ? "Załóż nowy pokój" : "Dołącz do pokoju";
            leaveGameButton.Text = IsHost ? "Zamknij pokój" : "Opuść pokój";
            roomIdTextBox.ReadOnly = IsHost;
        }

        private async void enterGameButton_Click(object sender, EventArgs e)
        {
            await Task.Run(() => EnterGame?.Invoke(this, EventArgs.Empty));
        }

        private async void leaveGameButton_Click(object sender, EventArgs e)
        {
            await Task.Run(() => LeaveGame?.Invoke(this, EventArgs.Empty));
        }

        private async void surrenderButton_Click(object sender, EventArgs e)
        {
            await Task.Run(() => Surrender?.Invoke(this, EventArgs.Empty));
        }

        private async void startGameButton_Click(object sender, EventArgs e)
        {
            await Task.Run(() => StartGame?.Invoke(this, EventArgs.Empty));
        }

        private void startDateTimer_Tick(object sender, EventArgs e)
        {
            this.InvokeIfRequired(() => gameTimeLabel.Text = (DateTime.Now - StartDate).ToString(@"hh\:mm\:ss"));
        }
    }
}