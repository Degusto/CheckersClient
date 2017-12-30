using System;
using System.Windows.Forms;
using CheckersClient.Extensions;
using CheckersCommon.Enums;
using CheckersCommon.Presenters.Contracts;

namespace CheckersCommon.Views
{
    public partial class MainView : Form, IMainView
    {
        public MainView()
        {
            InitializeComponent();

            hostRadioButton.Checked = true;
        }

        public event EventHandler<EventArgs> Surrender;
        public event EventHandler<EventArgs> LeaveGame;
        public event EventHandler<EventArgs> EnterGame;
        public event EventHandler<EventArgs> StartGame;

        private bool IsHost => hostRadioButton.Checked;

        private void hostRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            enterGameButton.Text = IsHost ? "Załóż nowy pokój" : "Dołącz do pokoju";
            leaveGameButton.Text = IsHost ? "Zamknij pokój" : "Opuść pokój";
            roomIdTextBox.ReadOnly = IsHost;
        }

        private void enterGameButton_Click(object sender, EventArgs e)
        {
            EnterGame?.Invoke(this, EventArgs.Empty);
        }

        private void leaveGameButton_Click(object sender, EventArgs e)
        {
            LeaveGame?.Invoke(this, EventArgs.Empty);
        }

        private void surrenderButton_Click(object sender, EventArgs e)
        {
            Surrender?.Invoke(this, EventArgs.Empty);
        }

        private void startGameButton_Click(object sender, EventArgs e)
        {
            StartGame?.Invoke(this, EventArgs.Empty);
        }

        public void ShowInfo(string message)
        {
            MessageBox.Show(message);
        }

        public PlayerType PlayerType => IsHost ? PlayerType.Host : PlayerType.Guest;

        public string RoomId { get => roomIdTextBox.Text; set => this.InvokeIfRequired(() => roomIdTextBox.Text = value); }

        public bool CanChangePlayerType { set => this.InvokeIfRequired(() => hostRadioButton.Enabled = guestRadioButton.Enabled = value); }

        public bool CanEnterGame { set => this.InvokeIfRequired(() => enterGameButton.Enabled = value); }

        public bool CanLeaveGame { set => this.InvokeIfRequired(() => leaveGameButton.Enabled = value); }

        public bool CanSurrender { set => this.InvokeIfRequired(() => surrenderButton.Enabled = value); }

        public bool CanStartGame { set => this.InvokeIfRequired(() => startGameButton.Enabled = value); }
    }
}