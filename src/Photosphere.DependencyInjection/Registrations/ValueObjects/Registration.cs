using System;
using Photosphere.DependencyInjection.Lifetimes;

namespace Photosphere.DependencyInjection.Registrations.ValueObjects
{
    internal class Registration : IRegistration
    {
        public Type ServiceType { get; set; }

        public Type ImplementationType { get; set; }

        public Delegate InstantiateFunction { get; set; }

        public object Instance { get; set; }

        public Lifetime Lifetime { get; set; }
    }
}