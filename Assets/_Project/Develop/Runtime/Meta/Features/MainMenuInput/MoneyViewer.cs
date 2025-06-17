using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using System;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Meta.Features.MainMenuInput
{
    public class MoneyViewer
    {
        private WalletService _wallet;

        public MoneyViewer(WalletService wallet)
        {
            _wallet = wallet;
        }       

        public void Show()
        {
            foreach (CurrencyType currency in Enum.GetValues(typeof(CurrencyType)))
                Debug.Log($"{currency}: {_wallet.GetCurrency(currency).Value}"); 
        }        
    }
}
