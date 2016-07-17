using System;

namespace Photosphere.DependencyInjection.Attributes
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public class RegisterInterceptorAttribute : Attribute
    {
        public RegisterInterceptorAttribute(Type interceptorType, Type attributeType, Lifetime lifetime = Lifetime.PerRequest)
        {
            InterceptorType = interceptorType;
            AttributeType = attributeType;
            Lifetime = lifetime;
        }

        public Type InterceptorType { get; set; }

        public Type AttributeType { get; }

        public Lifetime Lifetime { get; }
    }
}