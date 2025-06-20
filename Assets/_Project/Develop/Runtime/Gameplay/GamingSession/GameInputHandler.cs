﻿using Assets._Project.Develop.Runtime.Meta.Infrastructure;
using Assets._Project.Develop.Runtime.Utilities.Updater;
using System;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay
{
    public class GameInputHandler : IUpdatable, IDisposable
    {
        public event Action AfterWonMainMenuKeyPressed;
        public event Action AfterLostRestartKeyPressed;

        private GameFinishEventMaker _gameFinishEventMaker;
        private GameReferee _gameReferee;
        private NonMonoBehUpdater _updater;
        private bool _isWorking;
        private bool _isWon;
        private bool _isLost;
        private KeyCode _mainMenuKeyCode = KeyCode.Space;
        private KeyCode _restartKeyCode = KeyCode.Space;

        public GameInputHandler(GameFinishEventMaker gameFinishEventMaker, NonMonoBehUpdater updater, GameReferee gameReferee)
        {
            _gameReferee = gameReferee;
            _gameFinishEventMaker = gameFinishEventMaker;
            _updater = updater;
            Initialize();
        }

        private void Initialize()
        {
            _isWorking = false;
            _isWon = false;
            _isLost = false;
   
            _updater.Add(this);
            _gameFinishEventMaker.Win += OnWinGame;
            _gameFinishEventMaker.Lost += OnLooseGame;
        }
            
            
        private void OnWinGame()
        {
            _isWon = true;
        }

        private void OnLooseGame()
        {
            _isLost = true;
        }

        public void Update()
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

            _gameFinishEventMaker.Win -= OnWinGame;
            _gameFinishEventMaker.Lost -= OnLooseGame;
        }
    }
}
