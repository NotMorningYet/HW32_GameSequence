using Assets._Project.Develop.Runtime.Gameplay.Infrastructure;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;
using System;

namespace Assets._Project.Develop.Runtime.Gameplay.GamingSession
{
    public class GameFinisher : IDisposable
    {
        private GameInputHandler _gameInputHandler;
        private SceneSwitcherService _sceneSwitcher;
        private ICoroutinePerformer _coroutinePerformer;
        private GameplayInputArgs _gameInputArgs;

        public GameFinisher( 
            GameInputHandler gameInputHandler, 
            SceneSwitcherService sceneSwitcher, 
            ICoroutinePerformer coroutinePerformer)
        {
            _gameInputHandler = gameInputHandler;
            _sceneSwitcher = sceneSwitcher;
            _coroutinePerformer = coroutinePerformer;

            _gameInputHandler.AfterWonMainMenuKeyPressed += OnWonMainMenuPressed;
            _gameInputHandler.AfterLostRestartKeyPressed += OnGameRestartPressed;
        }

        public void Initialize(GameplayInputArgs gameplayInputArgs)
        {
            _gameInputArgs = gameplayInputArgs;
        }

        public void Dispose()
        {
            _gameInputHandler.AfterWonMainMenuKeyPressed -= OnWonMainMenuPressed;
            _gameInputHandler.AfterLostRestartKeyPressed -= OnGameRestartPressed;
        }

        private void OnWonMainMenuPressed()
        {
            _coroutinePerformer.StartPerform(
                _sceneSwitcher.ProcessSwitchTo(
                Scenes.MainMenu));
        }

        private void OnGameRestartPressed()
        {
            _coroutinePerformer.StartPerform(
                _sceneSwitcher.ProcessSwitchTo(
                Scenes.GamePlay,
                _gameInputArgs));
        }
    }
}
