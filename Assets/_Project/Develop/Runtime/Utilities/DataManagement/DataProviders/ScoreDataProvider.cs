using Assets._Project.Develop.Runtime.Configs.Meta.ScoreCount;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagement;
using System.Collections.Generic;

namespace Assets._Project.Develop.Runtime.Utilities.DataManagement.DataProviders
{
    public class ScoreDataProvider : DataProvider<ScoreData>
    {
        private readonly ConfigsProviderService _configProviderService;
        public ScoreDataProvider(
            ISaveLoadService saveLoadService, 
            ConfigsProviderService configsProviderService) : base(saveLoadService)
        {
            _configProviderService = configsProviderService;
        }

        protected override ScoreData GetOriginData()
        {
            Dictionary<string, int> defaultScore = GetDefaultScore();

            return new ScoreData
            {
                ScoreCounterData = defaultScore
            };
        }

        private Dictionary<string, int> GetDefaultScore()
        {
            StartScoreCounterConfig startScoreCounterConfig = _configProviderService.GetConfig<StartScoreCounterConfig>();
            return new Dictionary<string, int>()
            {
                {"WinCount", startScoreCounterConfig.WinCount},
                {"LostCount", startScoreCounterConfig.LostCount}
            };
        }        
    }
}

