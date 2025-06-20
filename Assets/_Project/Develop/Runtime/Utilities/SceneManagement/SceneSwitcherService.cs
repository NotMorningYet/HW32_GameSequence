﻿using Assets._Project.Develop.Runtime.Infrastructure;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilities.LoadingScreen;
using System.Collections;
using System;
using Object = UnityEngine.Object;

namespace Assets._Project.Develop.Runtime.Utilities.SceneManagement
{
    public class SceneSwitcherService
    {
        private readonly SceneLoaderService _sceneLoaderService;
        private readonly ILoadingScreen _loadingScreen;
        private readonly DIContainer _projectContainer;

        public SceneSwitcherService(
            SceneLoaderService sceneLoaderService, 
            ILoadingScreen loadingScreen, 
            DIContainer projectContainer)
        {
            _sceneLoaderService = sceneLoaderService;
            _loadingScreen = loadingScreen;
            _projectContainer = projectContainer;
        }

        public IEnumerator ProcessSwitchTo(string sceneName, IInputSceneArgs sceneArgs = null)
        {
            _loadingScreen.Show();

            DisposeOldScene();

            yield return _sceneLoaderService.LoadAsync(Scenes.Empty);
            yield return _sceneLoaderService.LoadAsync(sceneName);

            SceneBootStrap sceneBootStrap = Object.FindObjectOfType<SceneBootStrap>();

            if (sceneBootStrap == null)
                throw new NullReferenceException(nameof(sceneBootStrap) + " not found");

            DIContainer sceneContainer = new DIContainer(_projectContainer);

            sceneBootStrap.ProcessRegistrations(sceneContainer, sceneArgs);

            sceneContainer.Initiaize();
            
            yield return sceneBootStrap.Initialize();

            _loadingScreen.Hide();            

            sceneBootStrap.Run();
        }

        private void DisposeOldScene()
        {
            var oldBootStrap = Object.FindObjectOfType<SceneBootStrap>();

            if (oldBootStrap is IDisposable disposableBootStrap)
                disposableBootStrap.Dispose();
        }
    }
}
