using System;
using System.Collections;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Utilities.DataManagement.DataRepo
{
    public class PlayerPrefsDataRepository : IDataRepository
    {
        public IEnumerator Exist(string key, Action<bool> onExistResult)
        {
            bool exist = PlayerPrefs.HasKey(key);

            onExistResult(exist);

            yield break;
        }

        public IEnumerator Read(string key, Action<string> onRead)
        {
            string text = PlayerPrefs.GetString(key);

            onRead(text);

            yield break;
        }

        public IEnumerator Remove(string key)
        {
            PlayerPrefs.DeleteKey(key);

            yield break;
        }

        public IEnumerator Write(string key, string serializedData)
        {
            PlayerPrefs.SetString(key, serializedData);

            yield break;
        }
    }
}
