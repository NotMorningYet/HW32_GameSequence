﻿using System;

namespace Assets._Project.Develop.Runtime.Infrastructure.DI
{
    public class Registration
    {
        private Func<DIContainer, object> _creator;
        private object _cachedInstance;

        public Registration(Func<DIContainer, object> creator) => _creator = creator;           
        
        public object CreateInstanceFrom(DIContainer container)
        {
            if (_cachedInstance != null)
                return _cachedInstance;

            if (_creator == null)
                throw new InvalidOperationException("There is no instance or creator");

            _cachedInstance = _creator.Invoke(container);

            return _cachedInstance;
        }
    }
}
