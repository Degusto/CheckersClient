using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CheckersClient.Extensions;
using CheckersCommon.Enums;
using CheckersCommon.Models;

namespace CheckersClient.Views.Controls
{
    public partial class CellControl : UserControl
    {
        private Pawn _pawn;
        private bool _isPlaceholder;

        public event EventHandler<EventArgs> MakeMove;
        public event EventHandler<EventArgs> CaptureMouse;
        public event EventHandler<EventArgs> ReleaseMouse;

        public CellControl()
        {
            InitializeComponent();
        }

        public bool CapturedMouse { get; set; }

        public Pawn Pawn
        {
            get
            {
                return _pawn;
            }
            set
            {
                this.InvokeIfRequired(() => SetPawn(value));
            }
        }

        private void SetPawn(Pawn pawn)
        {
            _pawn = pawn;

            panel.BackColor = pawn.Owner == PlayerType.Host ? Color.White : Color.Black;
            kingPanel.Visible = pawn.IsPromoted;
        }

        public bool IsPlaceholder
        {
            get
            {
                return _isPlaceholder;
            }
            set
            {
                _isPlaceholder = value;

                this.InvokeIfRequired(() => panel.BackColor = Color.Red);
                this.InvokeIfRequired(() => kingPanel.Visible = Visible && !_isPlaceholder);
            }
        }

        public bool PawnVisible
        {
            set
            {
                this.InvokeIfRequired(() => panel.Visible = value);
            }
        }

        private void OnPanelMouseClick(object sender, MouseEventArgs e)
        {
            if (IsPlaceholder)
            {
                if (MakeMove != null)
                {
                    MakeMove.Invoke(this, EventArgs.Empty);
                }

                return;
            }

            if (Pawn == null || !Pawn.AvailableMoves.Any())
            {
                return;
            }

            if (CapturedMouse)
            {
                CapturedMouse = false;

                if (ReleaseMouse != null)
                {
                    ReleaseMouse.Invoke(this, EventArgs.Empty);
                }
            }
            else
            {
                CapturedMouse = true;

                if (CaptureMouse != null)
                {
                    CaptureMouse.Invoke(this, EventArgs.Empty);
                }
            }
        }
    }
}