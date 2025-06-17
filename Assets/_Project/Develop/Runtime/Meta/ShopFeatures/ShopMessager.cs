using System;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Meta.ShopFeatures
{
    public class ShopMessager : IDisposable
    {
        private readonly Shop _shop;
        private readonly string _notEnoughToBuyMessage = "В кошельке недостаточно средств для покупки";

        public ShopMessager(Shop shop)
        {
            _shop = shop;
            _shop.NotEnoughToBuy += OnNotEnoughToBuy;
        }

        public void Dispose()
        {
            _shop.NotEnoughToBuy -= OnNotEnoughToBuy;            
        }

        private void OnNotEnoughToBuy()
        {
            Debug.Log(_notEnoughToBuyMessage);
        }
    }
}
