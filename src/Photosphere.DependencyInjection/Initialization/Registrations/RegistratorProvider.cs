using System.Collections.Concurrent;
using System.Reflection;

namespace Photosphere.DependencyInjection.Initialization.Registrations
{
    internal class RegistratorProvider : IRegistratorProvider
    {
        private readonly IAssemblyBoundedRegistrator _assemblyBoundedRegistrator;
        private readonly IInterceptorRegistrator _interceptorRegistrator;
        private readonly ConcurrentDictionary<Assembly, IRegistrator> _registratorsCache;

        public RegistratorProvider(
            IAssemblyBoundedRegistrator assemblyBoundedRegistrator,
            IInterceptorRegistrator interceptorRegistrator)
        {
            _assemblyBoundedRegistrator = assemblyBoundedRegistrator;
            _interceptorRegistrator = interceptorRegistrator;
            _registratorsCache = new ConcurrentDictionary<Assembly, IRegistrator>();
        }

        public IRegistrator Provide(Assembly assembly)
        {
            IRegistrator registrator;
            if (_registratorsCache.TryGetValue(assembly, out registrator))
            {
                return registrator;
            }
            registrator = new Registrator(_assemblyBoundedRegistrator, _interceptorRegistrator, assembly);
            _registratorsCache.AddOrUpdate(assembly, a => registrator, (a, r) => r);
            return registrator;
        }
    }
}