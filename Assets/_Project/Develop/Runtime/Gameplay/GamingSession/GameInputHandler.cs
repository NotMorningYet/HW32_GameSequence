using System;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay
{
    public class GameInputHandler : MonoBehaviour, IDisposable
    {
        public event Action AfterWonMainMenuKeyPressed;
        public event Action AfterLostRestartKeyPressed;

        private GameReferee _gameReferee;
        private bool _isWorking;
        private bool _isWon;
        private bool _isLost;
        private KeyCode _mainMenuKeyCode = KeyCode.Space;
        private KeyCode _restartKeyCode = KeyCode.Space;

        public void Initialize(GameReferee gameReferee)
        {
            _isWorking = false;
            _isWon = false;
            _isLost = false;
            _gameReferee = gameReferee;
            _gameReferee.WinGame += OnLooseGame;
            _gameReferee.LostGame += OnWinGame;
        }

        private void OnWinGame()
        {
            _isLost = true;
        }

        private void OnLooseGame()
        {
            _isWon = true;
        }

        private void Update()
        {
            if (_isWorking == false)
                return;

            if (_isWon)
            {
                if (Input.GetKeyDown(_mainMenuKeyCode))
                    AfterWonMainMenuKeyPressed?.Invoke();
                
                return;
            }

            if (_isLost)
            {
                if (Input.GetKeyDown(_restartKeyCode))
                    AfterLostRestartKeyPressed?.Invoke();

                return;
            }

            string input = Input.inputString;

            if (input.Length > 0)
            {
                char pressedChar = input[0];
                Debug.Log($"Введено: {pressedChar}");
                _gameReferee.CheckInput(pressedChar);
            }
        }

        public void StartWorking()
        {
            _isWorking = true;
        }

        public void Dispose()
        {
            AfterLostRestartKeyPressed = null;
            AfterWonMainMenuKeyPressed = null;

            _gameReferee.WinGame -= OnLooseGame;
            _gameReferee.LostGame -= OnWinGame;

            if (gameObject != null)
                Destroy(gameObject);
        }
    }
}
