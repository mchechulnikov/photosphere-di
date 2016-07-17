using System;
using System.Reflection;

namespace Photosphere.DependencyInjection.Initialization.Registrations
{
    internal class Registrator : IRegistrator
    {
        private readonly IAssemblyBoundedRegistrator _assemblyBoundedRegistrator;
        private readonly IInterceptorRegistrator _interceptorRegistrator;
        private readonly Assembly _assembly;

        public Registrator(
            IAssemblyBoundedRegistrator assemblyBoundedRegistrator,
            IInterceptorRegistrator interceptorRegistrator,
            Assembly assembly)
        {
            _assemblyBoundedRegistrator = assemblyBoundedRegistrator;
            _interceptorRegistrator = interceptorRegistrator;
            _assembly = assembly;
        }

        public IRegistrator Register<TService>(Lifetime lifetime = Lifetime.PerRequest)
        {
            Register(typeof(TService), lifetime);
            return this;
        }

        public IRegistrator Register(Type serviceType, Lifetime lifetime = Lifetime.PerRequest)
        {
            _assemblyBoundedRegistrator.Register(serviceType, _assembly, lifetime);
            return this;
        }

        public IRegistrator RegisterBy<TAttribute>(Lifetime lifetime = Lifetime.PerRequest) where TAttribute : Attribute
        {
            RegisterBy(typeof(TAttribute), lifetime);
            return this;
        }

        public IRegistrator RegisterBy(Type attributeType, Lifetime lifetime = Lifetime.PerRequest)
        {
            _assemblyBoundedRegistrator.RegisterBy(attributeType, _assembly, lifetime);
            return this;
        }

        public IRegistrator RegisterInterceptor<TInterceptor, TAttribute>()
        {
            _interceptorRegistrator.Register(typeof(TInterceptor), typeof(TAttribute), _assembly);
            return this;
        }
    }
}