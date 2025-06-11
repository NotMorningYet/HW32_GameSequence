using UnityEngine;

namespace Assets._Project.Develop.Runtime.Utilities.AssetsManagement
{
    public class ResourcesAssetsLoader
    {
        public T Load<T>(string resourcesPath) where T : Object
            => Resources.Load<T>(resourcesPath);
    }
}