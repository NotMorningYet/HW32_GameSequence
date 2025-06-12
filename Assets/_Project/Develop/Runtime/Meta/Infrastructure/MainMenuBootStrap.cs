using Assets._Project.Develop.Runtime.Infrastructure;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;
using Assets._Project.Develop.Runtime.Utilities.Updater;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Meta.Infrastructure
{
    public class MainMenuBootStrap : SceneBootStrap
    {
        private DIContainer _container;
        private NonMonoBehUpdater _updater;
        private ModeInputHandler _modeInputHandler;
        private WalletService _walletService;

        public override void ProcessRegistrations(DIContainer container, IInputSceneArgs sceneArgs = null)
        {
            _container = container;

            MainMenuContextRegistrations.Process(_container);
        }

        public override IEnumerator Initialize()
        {
            _updater = _container.Resolve<NonMonoBehUpdater>();
            _modeInputHandler = _container.Resolve<ModeInputHandler>();
            _updater.Add(_modeInputHandler);

            _walletService = _container.Resolve<WalletService>();
            yield break;
        }

        public override void Run()
        {
            Debug.Log("Press 1 to begin Digital sequence Game");
            Debug.Log("Press 2 to begin Literal sequence Game");
        }
    }
}
