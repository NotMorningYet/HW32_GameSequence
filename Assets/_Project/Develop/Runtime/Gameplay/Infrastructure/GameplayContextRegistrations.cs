using Assets._Project.Develop.Runtime.Gameplay.GamingSession;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilities.AssetsManagement;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagement;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets._Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayContextRegistrations
    {
        public static void Process(DIContainer container, GameplayInputArgs args)
        {
            Debug.Log("Регистриаця сервисов на сцене геймплея");
            container.RegisterAsSingle(CreateGameReferee);
            container.RegisterAsSingle(CreateGameStarter);
            container.RegisterAsSingle(CreateSequenceGenerator);
            container.RegisterAsSingle(CreateGameInputHanlder);
            container.RegisterAsSingle(CreateGameFinisher);
            container.RegisterAsSingle(CreateGameResultView);
        }

        private static GameInputHandler CreateGameInputHanlder(DIContainer container)
        {
            ResourcesAssetsLoader resourcesAssetsLoader = container.Resolve<ResourcesAssetsLoader>();
            GameInputHandler gameInputHandlerPrefab = resourcesAssetsLoader.Load<GameInputHandler>("Utilities/GameInputHandler");
            GameInputHandler handler = Object.Instantiate(gameInputHandlerPrefab);
            handler.Initialize(container.Resolve<GameReferee>());

            return handler;
        }

        private static SequenceGenerator CreateSequenceGenerator(DIContainer container)
            => new SequenceGenerator(container.Resolve<ConfigsProviderService>());

        private static GameStarter CreateGameStarter(DIContainer container)
            => new GameStarter(
                container.Resolve<SequenceGenerator>(),
                container.Resolve<GameReferee>(),
                container.Resolve<GameInputHandler>()
            );

        private static GameFinisher CreateGameFinisher(DIContainer container)
            => new GameFinisher(
                container.Resolve<GameInputHandler>(),
                container.Resolve<SceneSwitcherService>(),
                container.Resolve<ICoroutinePerformer>()
                );

        private static GameReferee CreateGameReferee(DIContainer container)
            => new GameReferee();

        private static GameResultView CreateGameResultView(DIContainer container)
            =>new GameResultView(container.Resolve<GameReferee>());
           
    }


}



