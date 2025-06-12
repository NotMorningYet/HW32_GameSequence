using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilities.AssetsManagement;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;
using Assets._Project.Develop.Runtime.Utilities.Updater;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Meta.Infrastructure
{
    public class MainMenuContextRegistrations
    {
        public static void Process(DIContainer container)
        {
            container.RegisterAsSingle(CreateUpdater);
            container.RegisterAsSingle(CreateModeInputHandler);
        }

        private static NonMonoBehUpdater CreateUpdater(DIContainer container)
        {
            ResourcesAssetsLoader resourcesAssetsLoader = container.Resolve<ResourcesAssetsLoader>();
            NonMonoBehUpdater updaterPrefab = resourcesAssetsLoader.Load<NonMonoBehUpdater>("Utilities/NonMonoBehUpdater");
            NonMonoBehUpdater updater = Object.Instantiate(updaterPrefab);
            return updater;
        }

        private static ModeInputHandler CreateModeInputHandler(DIContainer container)
        {
            ModeInputHandler handler = new ModeInputHandler(
                container.Resolve<SceneSwitcherService>(),
                container.Resolve<ICoroutinePerformer>());

            return handler;
        }
    }
}
