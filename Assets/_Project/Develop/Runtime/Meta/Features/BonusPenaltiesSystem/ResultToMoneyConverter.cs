using Assets._Project.Develop.Runtime.Configs.Meta.BonusesPenalties;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.Meta.Infrastructure;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagement;
using System;

namespace Assets._Project.Develop.Runtime.Meta.Features.BonusPenaltiesSystem
{
    public class ResultToMoneyConverter : IDisposable
    {
        private GameFinishEventMaker _eventMaker;
        private ConfigsProviderService _configProviderService;
        private WalletService _walletService;
        private BonusesPenaltiesConfig _config;

        public ResultToMoneyConverter(GameFinishEventMaker eventMaker, ConfigsProviderService configProviderService, WalletService walletService)
        {
            _eventMaker = eventMaker;
            _configProviderService = configProviderService;
            _walletService = walletService;

            Initialize();
        }

        private void Initialize()
        {
            _config = _configProviderService.GetConfig<BonusesPenaltiesConfig>();
            _eventMaker.Win += OnWin;
            _eventMaker.Lost += OnLost;
        }

        private void OnLost()
        {
            foreach (CurrencyType currency in Enum.GetValues(typeof(CurrencyType)))
                _walletService.Spend(currency, _config.GetLostValue(currency));
        }

        private void OnWin()
        {
            foreach (CurrencyType currency in Enum.GetValues(typeof(CurrencyType)))
                _walletService.Add(currency, _config.GetWinValue(currency));
        }

        public void Dispose()
        {
            _eventMaker.Win -= OnWin;
            _eventMaker.Lost -= OnLost;
            _config = null;
        }
    }
}
