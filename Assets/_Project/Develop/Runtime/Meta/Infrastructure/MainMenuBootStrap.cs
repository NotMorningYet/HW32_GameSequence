using Assets._Project.Develop.Runtime.Infrastructure;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Meta.Features.MainMenuInput;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;
using Assets._Project.Develop.Runtime.Utilities.Updater;
using System;
using System.Collections;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Meta.Infrastructure
{
    public class MainMenuBootStrap : SceneBootStrap, IDisposable
    {
        private DIContainer _container;
        private MainMenuInformator _mainMenuInformator;

        public override void ProcessRegistrations(DIContainer container, IInputSceneArgs sceneArgs = null)
        {
            _container = container;

            MainMenuContextRegistrations.Process(_container);
        }

        public override IEnumerator Initialize()
        {            
            _mainMenuInformator = _container.Resolve<MainMenuInformator>();
            yield break;
        }

        public override void Run()
        {
            _mainMenuInformator.ShowInfo();
        }

        public void Dispose()
        {
            _container.Dispose();
        }
    }
}
