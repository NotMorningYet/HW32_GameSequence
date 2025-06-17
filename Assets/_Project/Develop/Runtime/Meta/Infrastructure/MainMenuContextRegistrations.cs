using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Meta.Features.MainMenuInput;
using Assets._Project.Develop.Runtime.Meta.Features.ScoreCount;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.Meta.ShopFeatures;
using Assets._Project.Develop.Runtime.Utilities.AssetsManagement;
using Assets._Project.Develop.Runtime.Utilities.DataManagement.DataProviders;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;
using Assets._Project.Develop.Runtime.Utilities.Updater;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Meta.Infrastructure
{
    public class MainMenuContextRegistrations
    {
        public static void Process(DIContainer container)
        {
            container.RegisterAsSingle(CreateUpdater).NonLazy();
            container.RegisterAsSingle(CreateShopMessager).NonLazy();
            container.RegisterAsSingle(CreateScoreCounterViewer).NonLazy();
            container.RegisterAsSingle(CreateMoneyViewer).NonLazy();
            container.RegisterAsSingle(CreateModeInputHandler).NonLazy();
            container.RegisterAsSingle(CreateButtonViewResultHandler).NonLazy();
            container.RegisterAsSingle(CreateButtonViewMoneytHandler).NonLazy();
            container.RegisterAsSingle(CreateResetScoreHandler).NonLazy();
            container.RegisterAsSingle(CreateButtonSaveLoadHandler).NonLazy();
            container.RegisterAsSingle(CreateMainMenuInformator).NonLazy();
        }

        private static MainMenuInformator CreateMainMenuInformator(DIContainer container)
            => new MainMenuInformator();

        private static ButtonSaveLoadHandler CreateButtonSaveLoadHandler(DIContainer container)
            => new ButtonSaveLoadHandler(container.Resolve<ICoroutinePerformer>(), container.Resolve<ScoreDataProvider>(), container.Resolve<NonMonoBehUpdater>());

        private static MoneyViewer CreateMoneyViewer(DIContainer container) => new MoneyViewer(container.Resolve<WalletService>());

        private static ButtonViewScoreCounterHandler CreateButtonViewResultHandler(DIContainer container)
            => new ButtonViewScoreCounterHandler(
                container.Resolve<NonMonoBehUpdater>(),
                container.Resolve<ScoreCounterViewer>()
                );

        private static ButtonViewMoneyHandler CreateButtonViewMoneytHandler(DIContainer container)
            => new ButtonViewMoneyHandler(
                container.Resolve<NonMonoBehUpdater>(),
                container.Resolve<MoneyViewer>()
                );

        private static ResetScoreHandler CreateResetScoreHandler(DIContainer container)
            => new ResetScoreHandler(container.Resolve<Shop>(), container.Resolve<ScoreCounter>(), container.Resolve<NonMonoBehUpdater>());

        private static ShopMessager CreateShopMessager(DIContainer container) => new ShopMessager(container.Resolve<Shop>());

        private static ScoreCounterViewer CreateScoreCounterViewer(DIContainer container) => new ScoreCounterViewer(container.Resolve<ScoreCounter>());

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
                container.Resolve<ICoroutinePerformer>(),
                container.Resolve<NonMonoBehUpdater>()
                );

            return handler;
        }
    }
}
