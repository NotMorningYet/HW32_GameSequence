using Assets._Project.Develop.Runtime.Configs.Meta.Wallet;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagement;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Meta.Features.Wallet
{
    public class WalletService 
    {
        private readonly Dictionary<CurrencyType, ReactiveVariable<int>> _currencies;
        private readonly ConfigsProviderService _configProvider;

        public WalletService(
            Dictionary<CurrencyType, ReactiveVariable<int>> currencies,            
            ConfigsProviderService configsProviderService)
        {
            _currencies = new Dictionary<CurrencyType, ReactiveVariable<int>>(currencies);
            _configProvider = configsProviderService;            

            Initialize();
        }

        private void Initialize()
        {
            StartWalletConfig config = _configProvider.GetConfig<StartWalletConfig>();

            foreach (var currencyConfig in config.GetAllCurrencies())
            {
                CurrencyType currencyType = currencyConfig.Type;
                int value = currencyConfig.Value;

                if (_currencies.ContainsKey(currencyType))
                    _currencies[currencyType].Value = value;
                else
                    _currencies.Add(currencyType, new ReactiveVariable<int>(value));
            }
        }

        public List<CurrencyType> AvailableCurrencies => _currencies.Keys.ToList();

        public IReadOnlyVariable<int> GetCurrency(CurrencyType type) => _currencies[type];

        public bool IsEnough(CurrencyType type, int amount)
        {
            if (amount < 0) 
                throw new ArgumentOutOfRangeException(nameof(amount));

            return _currencies[type].Value >= amount;
        }

        public void Add(CurrencyType type, int amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount));

            _currencies[type].Value += amount;
        }

        public void Spend(CurrencyType type, int amount)
        {
            if(IsEnough(type, amount) == false)
            {
                Debug.Log("Not enough: " + type.ToString());
                return;
            }

            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount));

            _currencies[type].Value -= amount;
        }
    }
}
