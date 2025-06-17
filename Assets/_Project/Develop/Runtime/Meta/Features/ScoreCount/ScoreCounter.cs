using Assets._Project.Develop.Runtime.Configs.Meta.ScoreCount;
using Assets._Project.Develop.Runtime.Meta.Infrastructure;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagement;
using Assets._Project.Develop.Runtime.Utilities.DataManagement;
using Assets._Project.Develop.Runtime.Utilities.DataManagement.DataProviders;
using System;

namespace Assets._Project.Develop.Runtime.Meta.Features.ScoreCount
{
    public class ScoreCounter : IDataReadable<ScoreData>, IDataWritable<ScoreData>, IDisposable
    {
        public event Action ValueChanged;

        private int _winCount;
        private int _lostCount;

        private GameFinishEventMaker _gameFinishEventMaker;
        private readonly ConfigsProviderService _configProviderService;

        public ScoreCounter(GameFinishEventMaker gameFinishEventMaker, ConfigsProviderService configsProviderService, ScoreDataProvider scoreDataProvider)
        {
            _gameFinishEventMaker = gameFinishEventMaker;
            _configProviderService = configsProviderService;

            scoreDataProvider.RegisterWriter(this);
            scoreDataProvider.RegisterReader(this);

            Initialize();
        }

        public int WinCount => _winCount;
        public int LostCount => _lostCount;

        private void Initialize()
        {
            ResetCount();

            _gameFinishEventMaker.Win += OnWinGame;
            _gameFinishEventMaker.Lost += OnLostGame;
        }

        private void OnLostGame()
        {
            _lostCount++;
            ValueChanged?.Invoke();
        }

        private void OnWinGame()
        {
            _winCount++;
            ValueChanged?.Invoke();
        }

        public void ReadFrom(ScoreData data)
        {
            _winCount = data.ScoreCounterData["WinCount"];
            _lostCount = data.ScoreCounterData["LostCount"];
        }

        public void WriteTo(ScoreData data)
        {
            data.ScoreCounterData["WinCount"] = _winCount;
            data.ScoreCounterData["LostCount"] = _lostCount;
        }

        public void Dispose()
        {
            _gameFinishEventMaker.Win -= OnWinGame;
            _gameFinishEventMaker.Lost -= OnLostGame;
            ValueChanged = null;
        }

        public void ResetCount()
        {
            StartScoreCounterConfig config = _configProviderService.GetConfig<StartScoreCounterConfig>();
            _winCount = config.WinCount;
            _lostCount = config.LostCount;
            ValueChanged?.Invoke();
        }
    }
}
