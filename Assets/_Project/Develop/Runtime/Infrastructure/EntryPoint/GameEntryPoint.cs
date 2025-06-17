using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Meta.Features.ScoreCount;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.Meta.Infrastructure;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagement;
using Assets._Project.Develop.Runtime.Utilities.DataManagement.DataProviders;
using Assets._Project.Develop.Runtime.Utilities.LoadingScreen;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;
using System.Collections;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Infrastructure.EntryPoint
{
    public class GameEntryPoint : MonoBehaviour
    {
        private void Awake()
        {
            SetupAppSettings();

            DIContainer projectContainer = new DIContainer();

            ProjectContextRegistrations.Process(projectContainer);

            projectContainer.Initiaize();

            projectContainer.Resolve<ICoroutinePerformer>().StartPerform(Initialize(projectContainer));
        }

        private void SetupAppSettings()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60;
        }

        private IEnumerator Initialize(DIContainer container)
        {
            ILoadingScreen loadingScreen = container.Resolve<ILoadingScreen>();
            SceneSwitcherService sceneSwitcherService = container.Resolve<SceneSwitcherService>();
            ScoreDataProvider scoreDataProvider = container.Resolve<ScoreDataProvider>();
            GameFinishEventMaker gameFinishEventMaker = container.Resolve<GameFinishEventMaker>();

            loadingScreen.Show();

            Debug.Log("Начало инициалзации сервисов");

            yield return container.Resolve<ConfigsProviderService>().LoadAsync();

            WalletService walletService = container.Resolve<WalletService>();
            bool isScoreDataSaveExists = false;

            yield return scoreDataProvider.Exists(result => isScoreDataSaveExists = result);

            container.Resolve<ScoreCounter>();

            if (isScoreDataSaveExists)
                yield return scoreDataProvider.Load();
            else scoreDataProvider.Reset();

            loadingScreen.Hide();

            yield return sceneSwitcherService.ProcessSwitchTo(Scenes.MainMenu);
        }
    }
}
