using System;
using Photosphere.DependencyInjection.Lifetimes;

namespace Photosphere.DependencyInjection.Registrations.Attributes
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public class RegisterDependenciesByAttribute : Attribute
    {
        public RegisterDependenciesByAttribute(Type targetAttributeType, Lifetime lifetime = Lifetime.PerRequest)
        {
            TargetAttributeType = targetAttributeType;
            Lifetime = lifetime;
        }

        public Type TargetAttributeType { get; }

        public Lifetime Lifetime { get; }
    }
}