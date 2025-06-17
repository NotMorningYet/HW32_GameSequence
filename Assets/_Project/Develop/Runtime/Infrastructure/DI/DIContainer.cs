using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Infrastructure.DI
{
    public class DIContainer : IDisposable
    {
        private readonly Dictionary<Type, Registration> _container = new();

        private readonly List<Type> _requests = new();

        private readonly DIContainer _parent;

        private readonly List<IDisposable> _disposables = new();

        private bool _disposed;

        public DIContainer(DIContainer parent) => _parent = parent;

        public DIContainer() : this(null) { }

        public IRegistrationOptions RegisterAsSingle<T>(Func<DIContainer, T> creator)
        {
            if (IsAlreadyRegistered<T>())
            {
                throw new InvalidOperationException($"{typeof(T)} is already registered");
            }

            Registration registration = new Registration(container =>
            {
                T instance = creator.Invoke(container);

                if (instance is IDisposable disposable)
                    _disposables.Add(disposable);

                return instance;
            });

            _container.Add(typeof(T), registration);

            return registration;
        }

        public bool IsAlreadyRegistered<T>()
        {
            if(_container.ContainsKey(typeof(T)))
                return true;

            if(_parent != null)
                return _parent.IsAlreadyRegistered<T>();

            return false;
        }

        public T Resolve<T>()
        {
            if (_requests.Contains(typeof(T)))
                throw new InvalidOperationException($"Cycle resolve for {typeof(T)}");

            _requests.Add(typeof(T));

            try
            {
                if (_container.TryGetValue(typeof(T), out Registration registration))
                    return (T)registration.CreateInstanceFrom(this);

                if(_parent != null)
                    return _parent.Resolve<T>();
            }
            finally
            {
                _requests.Remove(typeof(T));
            }

            throw new InvalidOperationException($"Registration for {typeof(T)} not found");
        }

        public void Dispose()
        {
            if (_disposed) return;

            for (int i = _disposables.Count - 1; i >= 0; i--)
                _disposables[i]?.Dispose();

            _disposables.Clear();
            _container.Clear();
            _requests.Clear();

            _disposed = true;
        }

        public void Initiaize()
        {
            foreach (Registration registration in _container.Values)
            {
                if (registration.IsNonLazy)
                    registration.CreateInstanceFrom(this);
            }
        }
    }
}
