using System;
using System.Collections.Generic;

namespace Photosphere.DependencyInjection.Initialization.Registrations.ValueObjects
{
    internal class Registration : IRegistration
    {
        private readonly Func<Delegate> _generateInstanceProvidingFunction;
        private readonly List<Type> _implementationTypes;

        public Registration(
            Func<Delegate> generateInstanceProvidingFunction,
            List<Type> implementationTypes)
        {
            _generateInstanceProvidingFunction = generateInstanceProvidingFunction;
            _implementationTypes = implementationTypes;
        }

        public Type ServiceType { get; set; }

        public Type DirectImplementationType { get; set; }

        public IReadOnlyCollection<Type> ImplementationTypes => _implementationTypes;

        public Delegate InstanceProvidingFunction { get; private set; }

        public Lifetime Lifetime { get; set; }

        public bool IsEnumerable { get; set; }

        public void GenerateInstantiateFunction()
        {
            InstanceProvidingFunction = _generateInstanceProvidingFunction();
        }

        public void AddImplementationTypes(IEnumerable<Type> types)
        {
            foreach (var type in types)
            {
                _implementationTypes.Add(type);
            }
        }
    }
}