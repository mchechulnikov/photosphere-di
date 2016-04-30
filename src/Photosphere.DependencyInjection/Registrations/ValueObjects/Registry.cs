using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Photosphere.DependencyInjection.Registrations.ValueObjects
{
    internal class Registry : IRegistry
    {
        private readonly IDictionary<Type, IRegistration> _dictionary;

        public Registry()
        {
            _dictionary = new ConcurrentDictionary<Type, IRegistration>();
        }

        public void Add(IRegistration registration)
        {
            _dictionary.Add(registration.ServiceType, registration);
        }

        public bool Contains(Type type)
        {
            return _dictionary.ContainsKey(type);
        }

        public IRegistration this[Type type] => _dictionary[type];
    }
}