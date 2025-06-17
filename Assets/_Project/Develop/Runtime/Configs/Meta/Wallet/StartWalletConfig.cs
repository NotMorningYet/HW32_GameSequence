using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Configs.Meta.Wallet
{
    [CreateAssetMenu(menuName = "Configs/Wallet/NewStartWalletConfig", fileName = "StartWalletConfig")]

    public class StartWalletConfig : ScriptableObject
    {
        [SerializeField] private List<CurrencyConfig> _values;

        public int GetValueFor(CurrencyType currencyType)
            => _values.First(config => config.Type == currencyType).Value;

        public IEnumerable<CurrencyConfig> GetAllCurrencies() => _values;        
            
        [Serializable]
        public class CurrencyConfig
        {
            [field: SerializeField] public CurrencyType Type { get; private set; }
            [field: SerializeField] public int Value { get; private set; }
        }
    }
}
