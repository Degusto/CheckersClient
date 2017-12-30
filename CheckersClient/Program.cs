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
        private static ManualResetEvent CreateRoomSignal = new ManualResetEvent(false);
        private static ManualResetEvent JoinRoomSignal = new ManualResetEvent(false);
        private static ManualResetEvent LeaveRoomSignal = new ManualResetEvent(false);
        private static ManualResetEvent CloseRoomSignal = new ManualResetEvent(false);
        private static ManualResetEvent NewGameSignal = new ManualResetEvent(false);
        private static ManualResetEvent SurrenderSignal = new ManualResetEvent(false);

        private static string RoomId { get; set; }

        [STAThread]
        static void Main()
        {
            //var hostTask = Task.Run(() => RunHost());
            //var guestTask = Task.Run(() => RunGuest());

            //Task.WaitAll(hostTask, guestTask);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var view = new MainView();
            var presenter = new MainPresenter(view, new GameService(new GameClient("127.0.0.1", 12345)));

            Application.Run(view);
        }

        private static void RunGuest()
        {
            var gameService = new GameService(new GameClient("127.0.0.1", 12345));

            // Join room
            CreateRoomSignal.WaitOne();
            gameService.JoinRoom(RoomId);
            JoinRoomSignal.Set();

            // Surrender
            NewGameSignal.WaitOne();
            gameService.Surrender();
            SurrenderSignal.Set();

            // Leave room
            SurrenderSignal.WaitOne();
            gameService.LeaveRoom();
            LeaveRoomSignal.Set();
        }

        private static void RunHost()
        {
            var gameService = new GameService(new GameClient("127.0.0.1", 12345));

            // Create room
            RoomId = gameService.CreateRoom();
            CreateRoomSignal.Set();

            // New game
            JoinRoomSignal.WaitOne();
            gameService.StartGame();
            NewGameSignal.Set();

            // Close room
            LeaveRoomSignal.WaitOne();
            gameService.CloseRoom();
            RoomId = null;
            CloseRoomSignal.Set();
        }
    }
}
