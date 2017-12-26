using System.Windows.Forms;
using CheckersClient.Presenters.Contracts;

namespace CheckersClient.Views
{
    public partial class MainView : Form, IMainView
    {
        public MainView()
        {
            InitializeComponent();
        }
    }
}
