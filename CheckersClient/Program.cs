using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CheckersCommon.Models;
using CheckersCommon.Presenters;
using CheckersCommon.Views;

namespace CheckersCommon
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var view = new MainView();
            var presenter = new MainPresenter(view, new GameService(new GameClient("127.0.0.1", 12345)));

            Application.Run(view);
        }
    }
}
