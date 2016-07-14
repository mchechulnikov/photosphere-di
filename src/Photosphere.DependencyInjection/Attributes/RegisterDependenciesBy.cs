using System;
using Photosphere.DependencyInjection.Lifetimes;

namespace Photosphere.DependencyInjection.Attributes
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public class RegisterDependenciesByAttribute : Attribute
    {
        public RegisterDependenciesByAttribute(Type registrationAttributeType, Lifetime lifetime = Lifetime.PerRequest)
        {
            RegistrationAttributeType = registrationAttributeType;
            Lifetime = lifetime;
        }

        public Type RegistrationAttributeType { get; }

        public Lifetime Lifetime { get; }
    }
}