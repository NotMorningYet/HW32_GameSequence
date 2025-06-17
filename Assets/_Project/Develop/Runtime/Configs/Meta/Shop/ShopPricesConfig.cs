using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using System;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Configs.Meta.Shop
{
    [CreateAssetMenu(menuName = "Configs/Shop/NewShopPricesConfig", fileName = "ShopPricesConfig")]

    public class ShopPricesConfig : ScriptableObject
    {
        [Serializable]
        public class ShopItem
        {
            [SerializeField] private string _name;
            [SerializeField] private int _price;
            [SerializeField] private CurrencyType _currencyType;

            public string Name => _name;
            public int Price => _price;
            public CurrencyType Currency => _currencyType;
        }

        [SerializeField] private ShopItem[] _shopItems;

        public ShopItem[] ShopItems => _shopItems;

        public ShopItem GetItemByName(string name)
        {
            foreach (var item in _shopItems)
            {
                if (item.Name == name)
                    return item;
            }
            return null;
        }
    }
}
