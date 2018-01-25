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
            var presenter = new MainPresenter(view, new GameService(new GameClient("192.168.104.11", 1234)));

            Application.Run(view);
        }
    }
}
