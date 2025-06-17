using Assets._Project.Develop.Runtime.Gameplay.GamingSession;
using Assets._Project.Develop.Runtime.Infrastructure;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Meta.Features.BonusPenaltiesSystem;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;
using System;
using System.Collections;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class GamePlayBootStrap : SceneBootStrap, IDisposable
    {
        private DIContainer _container;
        private GameplayInputArgs _inputArgs;
        private GameStarter _gameStarter;
        private GameFinisher _gameFinisher;
        private GameInputHandler _inputHandler;
        private GameResultView _resultView;
        private ResultToMoneyConverter _resultToMoneyConverter;

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
            _resultView = _container.Resolve<GameResultView>();
            _gameStarter = _container.Resolve<GameStarter>();
            _gameFinisher = _container.Resolve<GameFinisher>();
            _inputHandler = _container.Resolve<GameInputHandler>();
            _resultToMoneyConverter = _container.Resolve<ResultToMoneyConverter>();

            _gameFinisher.Initialize(_inputArgs);

            yield break;
        }

        public override void Run()
        {
            _gameStarter.StartGame(_inputArgs.ModeType);
        }

        public void Dispose()
        {
            _container.Dispose();
        }
    }
}
