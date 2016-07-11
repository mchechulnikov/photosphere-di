using System;
using Photosphere.DependencyInjection.Lifetimes;

namespace Photosphere.DependencyInjection.Registrations.Attributes
{
    public class RegisterDependencyAttribute : Attribute
    {
        public RegisterDependencyAttribute(Type serviceType, Lifetime lifetime = Lifetime.PerRequest)
        {
            ServiceType = serviceType;
            Lifetime = lifetime;
        }

        public Type ServiceType { get; }

        public Lifetime Lifetime { get; }
    }
}