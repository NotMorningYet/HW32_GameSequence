using Assets._Project.Develop.Runtime.Utilities.Reactive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets._Project.Develop.Runtime.Meta.Features.Wallet
{
    public class WalletService
    {
        private readonly Dictionary<CurrencyType, ReactiveVariable<int>> _currencies;

        public WalletService(Dictionary<CurrencyType, ReactiveVariable<int>> currenies)
        {
            _currencies = new Dictionary<CurrencyType, ReactiveVariable<int>>(currenies);
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
                throw new InvalidOperationException("Not enough: " + type.ToString());

            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount));

            _currencies[type].Value -= amount;
        }
    }
}
