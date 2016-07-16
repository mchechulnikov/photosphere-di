using System;
using System.Collections.Generic;

namespace Photosphere.DependencyInjection.Initialization.Registrations.ValueObjects
{
    internal class Registration : IRegistration
    {
        private readonly Func<Delegate> _generateInstanceProvidingFunction;

        public Registration(
            Func<Delegate> generateInstanceProvidingFunction)
        {
            _generateInstanceProvidingFunction = generateInstanceProvidingFunction;
        }

        public Type ServiceType { get; set; }

        public Type DirectImplementationType { get; set; }

        public IReadOnlyCollection<Type> ImplementationTypes { get; set; }

        public Delegate InstanceProvidingFunction { get; private set; }

        public Lifetime Lifetime { get; set; }

        public bool IsEnumerable { get; set; }

        public void GenerateInstantiateFunction()
        {
            InstanceProvidingFunction = _generateInstanceProvidingFunction();
        }
    }
}