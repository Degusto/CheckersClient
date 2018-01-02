using System.Drawing;
using System.Windows.Forms;
using CheckersCommon.Enums;

namespace CheckersClient.Views.Controls
{
    public partial class PawnControl : UserControl
    {
        public PawnControl()
        {
            InitializeComponent();
        }

        public PlayerType PlayerType
        {
            set => panel.BackColor = value == PlayerType.Host ? Color.White : Color.Black;
        }
    }
}
