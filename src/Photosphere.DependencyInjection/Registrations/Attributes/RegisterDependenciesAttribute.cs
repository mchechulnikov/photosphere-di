using System;
using Photosphere.DependencyInjection.Lifetimes;

namespace Photosphere.DependencyInjection.Registrations.Attributes
{
    public class RegisterDependenciesAttribute : Attribute
    {
        public RegisterDependenciesAttribute(Type serviceType, Lifetime lifetime = Lifetime.PerRequest)
        {
            ServiceType = serviceType;
            Lifetime = lifetime;
        }

        public Type ServiceType { get; }

        public Lifetime Lifetime { get; }
    }
}