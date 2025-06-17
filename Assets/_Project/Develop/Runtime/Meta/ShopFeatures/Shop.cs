using Assets._Project.Develop.Runtime.Configs.Meta.Shop;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagement;
using System;

namespace Assets._Project.Develop.Runtime.Meta.ShopFeatures
{
    public class Shop : IDisposable
    {
        public event Action NotEnoughToBuy;

        private WalletService _walletService;
        private readonly ConfigsProviderService _configProvider;
        private ShopPricesConfig _shopPricesConfig;

        public Shop(WalletService walletService, ConfigsProviderService configProvider)
        {
            _walletService = walletService;
            _configProvider = configProvider;

            Initialize();
        }

        private void Initialize()
        {
            _shopPricesConfig = _configProvider.GetConfig<ShopPricesConfig>();
        }

        public bool Buy(string name)
        {
            var item = _shopPricesConfig.GetItemByName(name);

            if (item == null)
                return false;

            if (_walletService.IsEnough(item.Currency, item.Price))
            {
                _walletService.Spend(item.Currency, item.Price);
                return true;
            }
            else
            {
                NotEnoughToBuy?.Invoke();
                return false;
            }
        }

        public void Dispose()
        {
            NotEnoughToBuy = null;
        }
    }
}
