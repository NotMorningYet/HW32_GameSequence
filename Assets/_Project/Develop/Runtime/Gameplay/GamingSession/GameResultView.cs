using System;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.GamingSession
{
    public class GameResultView : IDisposable
    {
        private readonly string _winMessage = "Победа. Нажмите Пробел для выхода в Главное меню";
        private readonly string _looseMessage = "Поражение. Нажмите пробел, чтобы попробовать еще раз";
        private GameReferee _gameReferee;

        public GameResultView(GameReferee gameReferee)
        {
            _gameReferee = gameReferee;

            _gameReferee.WinGame += OnWinGame;
            _gameReferee.LostGame += OnLooseGame;
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
            _gameReferee.WinGame -= OnWinGame;
            _gameReferee.LostGame -= OnLooseGame;
        }
    }
}
