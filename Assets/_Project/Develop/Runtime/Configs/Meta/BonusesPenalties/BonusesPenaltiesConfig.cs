using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Configs.Meta.BonusesPenalties
{
    [CreateAssetMenu(menuName = "Configs/BonusPenaltiy/NewBonusPenaltyConfig", fileName = "BonusPenaltyConfig")]

    public class BonusesPenaltiesConfig : ScriptableObject
    {
        [SerializeField] private List<WinCurrencyConfig> _winValues;
        [SerializeField] private List<LostCurrencyConfig> _lostValues;

        public int GetWinValue(CurrencyType currencyType)
            => _winValues.First(config => config.Type == currencyType).Value;

        public int GetLostValue(CurrencyType currencyType)
            => _lostValues.First(config => config.Type == currencyType).Value;

        [Serializable]
        public class WinCurrencyConfig
        {
            [field: SerializeField] public CurrencyType Type { get; private set; }
            [field: SerializeField] public int Value { get; private set; }
        }

        [Serializable]
        public class LostCurrencyConfig
        {
            [field: SerializeField] public CurrencyType Type { get; private set; }
            [field: SerializeField] public int Value { get; private set; }
        }
    }
}
