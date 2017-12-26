using CheckersClient.Models;
using CheckersClient.Presenters.Contracts;
using CheckersClient.Utilities;

namespace CheckersClient.Presenters
{
    internal sealed class MainPresenter
    {
        private readonly IMainView _view;
        private readonly IServer _server;

        internal MainPresenter(IMainView view, IServer server)
        {
            _view = view.NotNull();
            _server = server.NotNull();
        }
    }
}