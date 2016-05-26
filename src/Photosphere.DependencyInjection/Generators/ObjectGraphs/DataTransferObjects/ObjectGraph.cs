using System;
using System.Collections.Generic;
using System.Reflection;
using Photosphere.DependencyInjection.Registrations.ValueObjects;

namespace Photosphere.DependencyInjection.Generators.ObjectGraphs.DataTransferObjects
{
    internal class ObjectGraph : IObjectGraph
    {
        private readonly IRegistration _registration;

        public ObjectGraph(
            IRegistration registration,
            ConstructorInfo constructor,
            IReadOnlyList<IObjectGraph> children)
        {
            _registration = registration;
            Constructor = constructor;
            Children = children;
        }

        public Type ImplementationType => _registration.ImplementationType;

        public object RegisteredInstance => _registration.Instance;

        public ConstructorInfo Constructor { get; }

        public IReadOnlyList<IObjectGraph> Children { get; }
    }
}