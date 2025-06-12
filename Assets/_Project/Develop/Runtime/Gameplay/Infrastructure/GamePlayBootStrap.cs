using Assets._Project.Develop.Runtime.Gameplay.GamingSession;
using Assets._Project.Develop.Runtime.Infrastructure;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;
using Assets._Project.Develop.Runtime.Utilities.Updater;
using System;
using System.Collections;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class GamePlayBootStrap : SceneBootStrap, IDisposable
    {
        private DIContainer _container;
        private NonMonoBehUpdater _updater;
        private GameplayInputArgs _inputArgs;
        private GameStarter _gameStarter;
        private GameFinisher _gameFinisher;
        private GameInputHandler _inputHandler;
        private GameResultView _gameResultView;

        public override void ProcessRegistrations(DIContainer container, IInputSceneArgs sceneArgs)
        {
            _container = container;

            if (sceneArgs is not GameplayInputArgs gameplayInputArgs)
                throw new ArgumentException($"{nameof(sceneArgs)} is not match with {typeof(GameplayInputArgs)} type");

            _inputArgs = gameplayInputArgs;

            GameplayContextRegistrations.Process(_container, _inputArgs);
        }

        public override IEnumerator Initialize()
        {
            Debug.Log($"Type: {_inputArgs.ModeType}");
            
            _updater = _container.Resolve<NonMonoBehUpdater>();
            _gameStarter = _container.Resolve<GameStarter>();
            _gameFinisher = _container.Resolve<GameFinisher>();
            _gameFinisher.Initialize(_inputArgs);
            _gameResultView = _container.Resolve<GameResultView>();
            _inputHandler = _container.Resolve<GameInputHandler>();

            _updater.Add(_inputHandler);

            yield break;
        }

        public override void Run()
        {
            Debug.Log("Start gameplayScene");
            _gameStarter.StartGame(_inputArgs.ModeType);
        }

        public void Dispose()
        {
            _container.Dispose();
            Destroy(gameObject);
        }
    }
}
