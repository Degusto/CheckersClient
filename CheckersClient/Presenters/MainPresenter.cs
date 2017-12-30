using System;
using CheckersCommon.Enums;
using CheckersCommon.Models;
using CheckersCommon.Presenters.Contracts;
using CheckersCommon.Utilities;

namespace CheckersCommon.Presenters
{
    internal sealed class MainPresenter
    {
        private bool _isInRoom = false;

        private readonly IMainView _view;
        private readonly IGameService _gameService;

        internal MainPresenter(IMainView view, IGameService gameService)
        {
            _view = view.NotNull();
            _gameService = gameService.NotNull();

            _view.EnterGame += OnEnterGame;
            _view.LeaveGame += OnLeaveGame;
            _view.Surrender += OnSurrender;
            _view.StartGame += OnStartGame;

            _view.CanChangePlayerType = true;
            _view.CanEnterGame = true;
            _view.CanLeaveGame = false;
            _view.CanSurrender = false;
            _view.CanStartGame = false;

            _gameService.PlayerJoined += OnPlayerJoined;
            _gameService.PlayerLeft += OnPlayerLeft;
            _gameService.GameStarted += OnGameStarted;
            _gameService.PlayerWithdrew += OnPlayerWithdrew;
        }

        private void OnStartGame(object sender, EventArgs e)
        {
            _gameService.StartGame();

            _view.CanLeaveGame = false;
            _view.CanSurrender = true;
            _view.CanStartGame = false;
        }

        private void OnSurrender(object sender, EventArgs e)
        {
            _gameService.Surrender();
            _view.CanSurrender = false;
            _view.CanLeaveGame = _view.PlayerType == PlayerType.Guest;
            _view.CanStartGame = _view.PlayerType == PlayerType.Host;
        }

        private void OnPlayerWithdrew(object sender, EventArgs e)
        {
            _view.CanSurrender = false;
            _view.CanLeaveGame = _view.PlayerType == PlayerType.Guest;
            _view.CanStartGame = _view.PlayerType == PlayerType.Host;
        }

        private void OnGameStarted(object sender, EventArgs e)
        {
            _view.CanLeaveGame = false;
            _view.CanSurrender = true;
        }

        private void OnPlayerJoined(object sender, EventArgs e)
        {
            _view.CanStartGame = true;
            _view.CanLeaveGame = false;
        }

        private void OnPlayerLeft(object sender, EventArgs e)
        {
            _view.CanStartGame = false;
            _view.CanLeaveGame = true;
        }

        private void OnEnterGame(object sender, EventArgs e)
        {
            if (_isInRoom)
            {
                _view.ShowInfo("Już jesteś w grze!");

                return;
            }

            if (_view.PlayerType == PlayerType.Host)
            {
                _view.RoomId = _gameService.CreateRoom();
            }
            else
            {
                _gameService.JoinRoom(_view.RoomId);
            }

            _view.CanChangePlayerType = false;
            _view.CanEnterGame = false;
            _view.CanLeaveGame = true;
            _isInRoom = true;
        }

        private void OnLeaveGame(object sender, EventArgs e)
        {
            if (!_isInRoom)
            {
                _view.ShowInfo("Nie jesteś w grze!");

                return;
            }

            if (_view.PlayerType == PlayerType.Host)
            {
                _gameService.CloseRoom();
                _view.RoomId = null;
            }
            else
            {
                _gameService.LeaveRoom();
            }

            _view.CanChangePlayerType = true;
            _view.CanEnterGame = true;
            _view.CanSurrender = false;
            _view.CanLeaveGame = false;
            _isInRoom = false;
        }
    }
}