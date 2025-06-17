﻿using Assets._Project.Develop.Runtime.Utilities.DataManagement.DataRepo;
using Assets._Project.Develop.Runtime.Utilities.DataManagement.KeyStorage;
using Assets._Project.Develop.Runtime.Utilities.DataManagement.Serializers;
using System;
using System.Collections;

namespace Assets._Project.Develop.Runtime.Utilities.DataManagement
{
    public class SaveLoadService : ISaveLoadService
    {
        private readonly IDataSerializer _serializer;
        private readonly IDataKeyStorage _keyStorage;
        private readonly IDataRepository _repository;

        public SaveLoadService(IDataSerializer serializer, IDataKeyStorage keyStorage, IDataRepository repository)
        {
            _serializer = serializer;
            _keyStorage = keyStorage;
            _repository = repository;
        }

        public IEnumerator Exists<TData>(Action<bool> onExistsResult) where TData : ISaveData
        {
            string key = _keyStorage.GetKeyFor<TData>();

            yield return _repository.Exist(key, result => onExistsResult?.Invoke(result));
        }

        public IEnumerator Load<TData>(Action<TData> onLoad) where TData : ISaveData
        {
            string key = _keyStorage.GetKeyFor<TData>();
            string serializedData = "";

            yield return _repository.Read(key, result => serializedData = result);

            TData data = _serializer.Deserialize<TData>(serializedData);    

            onLoad?.Invoke(data);
        }

        public IEnumerator Remove<TData>() where TData : ISaveData
        {
            string key = _keyStorage?.GetKeyFor<TData>();

            yield return _repository.Remove(key);
        }

        public IEnumerator Save<TData>(TData data) where TData : ISaveData
        {
            string serializedData = _serializer.Serialize(data);
            string key = _keyStorage.GetKeyFor<TData>();            

            yield return _repository.Write(key, serializedData);
        }
    }
}
