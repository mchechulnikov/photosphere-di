using System;

namespace Photosphere.DependencyInjection.Attributes
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
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