using Assets._Project.Develop.Runtime.Meta.Infrastructure;
using System;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.GamingSession
{
    public class GameResultView : IDisposable
    {
        private readonly string _winMessage = "Победа. Нажмите Пробел для выхода в Главное меню";
        private readonly string _looseMessage = "Поражение. Нажмите пробел, чтобы попробовать еще раз";
        private GameFinishEventMaker _gameFinishEventMaker;

        public GameResultView(GameFinishEventMaker eventmaker)
        {
            _gameFinishEventMaker = eventmaker;

            _gameFinishEventMaker.Win += OnWinGame;
            _gameFinishEventMaker.Lost += OnLooseGame;
        }

        private void OnWinGame()
        {
            Debug.Log(_winMessage);
        }

        private void OnLooseGame()
        {
            Debug.Log(_looseMessage);
        }

        public void Dispose()
        {
            _gameFinishEventMaker.Win -= OnWinGame;
            _gameFinishEventMaker.Lost -= OnLooseGame;
        }
    }
}
