using System;
using System.Collections.Generic;
using System.Reflection;
using Photosphere.DependencyInjection.Lifetimes;
using Photosphere.DependencyInjection.Registrations.ValueObjects;

namespace Photosphere.DependencyInjection.Generators.ObjectGraphs.DataTransferObjects
{
    internal class ObjectGraph : IObjectGraph
    {
        private readonly IRegistration _registration;

        public ObjectGraph(
            IRegistration registration,
            ConstructorInfo constructor,
            IReadOnlyList<IObjectGraph> children = null)
        {
            _registration = registration;
            Constructor = constructor;
            Children = children ?? new List<IObjectGraph>();
        }

        public Type ReturnType => _registration.ServiceType;

        public Type ImplementationType => _registration.DirectImplementationType;

        public ConstructorInfo Constructor { get; }

        public Lifetime Lifetime => _registration.Lifetime;

        public IReadOnlyList<IObjectGraph> Children { get; }

        public bool IsEnumerable => _registration.IsEnumerable;
    }
}