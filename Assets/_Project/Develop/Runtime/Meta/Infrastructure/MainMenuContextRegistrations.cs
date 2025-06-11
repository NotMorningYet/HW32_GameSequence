using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilities.AssetsManagement;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Meta.Infrastructure
{
    public class MainMenuContextRegistrations
    {
        public static void Process(DIContainer container)
        {
            container.RegisterAsSingle(CreateInputHandler);
        }

        private static ModeInputHandler CreateInputHandler(DIContainer container)
        {
            ResourcesAssetsLoader resourcesAssetsLoader = container.Resolve<ResourcesAssetsLoader>();
            ModeInputHandler modeInputHandlerPrefab = resourcesAssetsLoader.Load<ModeInputHandler>("Utilities/ModeInputHandler");
            ModeInputHandler handler = Object.Instantiate(modeInputHandlerPrefab);
            handler.Initialize(container.Resolve<SceneSwitcherService>(),
                container.Resolve<ICoroutinePerformer>());

            return handler;
        }
    }
}
