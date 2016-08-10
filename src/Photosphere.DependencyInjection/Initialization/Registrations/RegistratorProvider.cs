using System.Collections.Concurrent;
using System.Reflection;

namespace Photosphere.DependencyInjection.Initialization.Registrations
{
    internal class RegistratorProvider : IRegistratorProvider
    {
        private readonly IAssemblyBoundedRegistrator _assemblyBoundedRegistrator;
        private readonly ConcurrentDictionary<Assembly, IRegistrator> _registratorsCache;

        public RegistratorProvider(
            IAssemblyBoundedRegistrator assemblyBoundedRegistrator)
        {
            _assemblyBoundedRegistrator = assemblyBoundedRegistrator;
            _registratorsCache = new ConcurrentDictionary<Assembly, IRegistrator>();
        }

        public IRegistrator Provide(Assembly assembly)
        {
            IRegistrator registrator;
            if (_registratorsCache.TryGetValue(assembly, out registrator))
            {
                return registrator;
            }
            registrator = new Registrator(_assemblyBoundedRegistrator, assembly);
            _registratorsCache.AddOrUpdate(assembly, a => registrator, (a, r) => r);
            return registrator;
        }
    }
}