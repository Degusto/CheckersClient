using CheckersCommon.Models;
using CheckersCommon.Presenters.Contracts;
using CheckersCommon.Utilities;

namespace CheckersCommon.Presenters
{
    internal sealed class MainPresenter
    {
        private readonly IMainView _view;
        private readonly IGameService _server;

        internal MainPresenter(IMainView view, IGameService server)
        {
            _view = view.NotNull();
            _server = server.NotNull();
        }
    }
}