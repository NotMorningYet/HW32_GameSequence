using Assets._Project.Develop.Runtime.Configs.Meta.BonusesPenalties;
using Assets._Project.Develop.Runtime.Configs.Meta.ScoreCount;
using Assets._Project.Develop.Runtime.Configs.Meta.Shop;
using Assets._Project.Develop.Runtime.Configs.Meta.Wallet;
using Assets._Project.Develop.Runtime.Utilities.AssetsManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Utilities.ConfigsManagement
{
    public class ResourcesConfigsLoader : IConfigsLoader
    {
        private readonly ResourcesAssetsLoader _resources;

        private readonly Dictionary<Type, string> _configsResourcesPaths = new()
        {
            {typeof(QuestTypeConfigTemplate), "Configs/QuestTypeConfig" },
            {typeof(StartWalletConfig), "Configs/StartWalletConfig" },
            {typeof(BonusesPenaltiesConfig), "Configs/BonusPenaltyConfig" },
            {typeof(StartScoreCounterConfig), "Configs/StartScoreCounterConfig" },
            {typeof(ShopPricesConfig), "Configs/ShopPricesConfig" }
        };

        public ResourcesConfigsLoader(ResourcesAssetsLoader resources)
        {
            _resources = resources;
        }

        public IEnumerator LoadAsync(Action<Dictionary<Type, object>> onConfigsLoaded)
        {
            Dictionary<Type, object> loadedConfigs = new();

            foreach (KeyValuePair<Type, string> configResourcesPath in _configsResourcesPaths)
            {
                ScriptableObject config = _resources.Load<ScriptableObject>(configResourcesPath.Value);
                loadedConfigs.Add(configResourcesPath.Key, config);

                yield return null;
            }

            onConfigsLoaded?.Invoke(loadedConfigs);
        }

    }
}