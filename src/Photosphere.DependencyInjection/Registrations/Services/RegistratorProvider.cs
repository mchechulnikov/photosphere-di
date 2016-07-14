using System.Collections.Generic;
using System.Reflection;

namespace Photosphere.DependencyInjection.Registrations.Services
{
    internal class RegistratorProvider : IRegistratorProvider
    {
        private readonly IAssemblyBoundedRegistrator _assemblyBoundedRegistrator;
        private readonly IDictionary<Assembly, IRegistrator> _registratorsCache;

        public RegistratorProvider(IAssemblyBoundedRegistrator assemblyBoundedRegistrator)
        {
            _assemblyBoundedRegistrator = assemblyBoundedRegistrator;
            _registratorsCache = new Dictionary<Assembly, IRegistrator>();
        }

        public IRegistrator Provide(Assembly assembly)
        {
            IRegistrator registrator;
            if (_registratorsCache.TryGetValue(assembly, out registrator))
            {
                return registrator;
            }
            registrator = new Registrator(_assemblyBoundedRegistrator, assembly);
            _registratorsCache.Add(assembly, registrator);
            return registrator;
        }
    }
}