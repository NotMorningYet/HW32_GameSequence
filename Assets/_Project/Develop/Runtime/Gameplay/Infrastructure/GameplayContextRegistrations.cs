using Assets._Project.Develop.Runtime.Gameplay.GamingSession;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Meta.Features.BonusPenaltiesSystem;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.Meta.Infrastructure;
using Assets._Project.Develop.Runtime.Utilities.AssetsManagement;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagement;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;
using Assets._Project.Develop.Runtime.Utilities.Updater;
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
            container.RegisterAsSingle(CreateResultToMoneyConverter);
            container.RegisterAsSingle(CreateUpdater).NonLazy();
        }

        private static ResultToMoneyConverter CreateResultToMoneyConverter(DIContainer container)
            => new ResultToMoneyConverter(
                container.Resolve<GameFinishEventMaker>(),
                container.Resolve<ConfigsProviderService>(),
                container.Resolve<WalletService>()
                );

        private static NonMonoBehUpdater CreateUpdater(DIContainer container)
        {
            ResourcesAssetsLoader resourcesAssetsLoader = container.Resolve<ResourcesAssetsLoader>();
            NonMonoBehUpdater updaterPrefab = resourcesAssetsLoader.Load<NonMonoBehUpdater>("Utilities/NonMonoBehUpdater");
            NonMonoBehUpdater updater = Object.Instantiate(updaterPrefab);
            return updater;
        }

        private static GameInputHandler CreateGameInputHanlder(DIContainer container) 
            => new GameInputHandler(container.Resolve<GameFinishEventMaker>(), container.Resolve<NonMonoBehUpdater>(), container.Resolve<GameReferee>());                                

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
            => new GameReferee(container.Resolve<GameFinishEventMaker>());

        private static GameResultView CreateGameResultView(DIContainer container)
            =>new GameResultView(container.Resolve<GameFinishEventMaker>());
           
    }


}



