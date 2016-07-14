using System;
using System.Reflection;
using Photosphere.DependencyInjection.Lifetimes;
using Photosphere.DependencyInjection.Registrations.Services;

namespace Photosphere.DependencyInjection
{
    internal class Registrator : IRegistrator
    {
        private readonly IAssemblyBoundedRegistrator _assemblyBoundedRegistrator;
        private readonly Assembly _assembly;

        public Registrator(
            IAssemblyBoundedRegistrator assemblyBoundedRegistrator,
            Assembly assembly)
        {
            _assemblyBoundedRegistrator = assemblyBoundedRegistrator;
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
    }
}