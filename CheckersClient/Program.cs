using System;
using CheckersCommon.Models;

namespace CheckersCommon
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            var gameService = new GameService(new GameClient("127.0.0.1", 12345));

            gameService.NewRoom();
            //gameService.LeaveRoom();

            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new MainView());
        }
    }
}
