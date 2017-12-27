using System.Windows.Forms;
using CheckersCommon.Presenters.Contracts;

namespace CheckersCommon.Views
{
    public partial class MainView : Form, IMainView
    {
        public MainView()
        {
            InitializeComponent();
        }
    }
}
