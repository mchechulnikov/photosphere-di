using System;

namespace Photosphere.DependencyInjection
{
    public interface IRegistrator
    {
        IRegistrator Register<TService>(Lifetime lifetime = Lifetime.PerRequest);

        IRegistrator Register(Type serviceType, Lifetime lifetime = Lifetime.PerRequest);

        IRegistrator RegisterBy<TAttribute>(Lifetime lifetime = Lifetime.PerRequest) where TAttribute : Attribute;

        IRegistrator RegisterBy(Type attributeType, Lifetime lifetime = Lifetime.PerRequest);

        IRegistrator RegisterInterceptor<TInterceptor, TAttribute>();
    }
}