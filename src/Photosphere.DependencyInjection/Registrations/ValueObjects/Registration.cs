using System;
using System.Collections.Generic;
using Photosphere.DependencyInjection.Lifetimes;

namespace Photosphere.DependencyInjection.Registrations.ValueObjects
{
    internal class Registration : IRegistration
    {
        private readonly Func<Delegate> _generateInstatiationFunction;

        public Registration(
            Func<Delegate> generateInstatiationFunction)
        {
            _generateInstatiationFunction = generateInstatiationFunction;
        }

        public Type ServiceType { get; set; }

        public Type DirectImplementationType { get; set; }

        public IReadOnlyCollection<Type> ImplementationTypes { get; set; }

        public Delegate InstantiateFunction { get; private set; }

        public Lifetime Lifetime { get; set; }

        public bool IsEnumerable { get; set; }

        public void GenerateInstantiateFunction()
        {
            InstantiateFunction = _generateInstatiationFunction();
        }
    }
}