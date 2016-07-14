using System.Reflection;
using Photosphere.DependencyInjection.Registrations.Services.CompositionRoots;
using Photosphere.DependencyInjection.Registrations.Services.CompositionRoots.ServiceCompositionRoots;

namespace Photosphere.DependencyInjection.Registrations.Services
{
    internal class DependenciesCompositor : IDependenciesCompositor
    {
        private readonly ICompositionRootProvider _compositionRootProvider;
        private readonly IRegistratorProvider _registratorProvider;

        public DependenciesCompositor(
            ICompositionRootProvider compositionRootProvider,
            IRegistratorProvider registratorProvider)
        {
            _compositionRootProvider = compositionRootProvider;
            _registratorProvider = registratorProvider;
        }

        public void Compose()
        {
            foreach (var compositionRoot in _compositionRootProvider.Provide())
            {
                var compositionRootAssembly = GetCompositionRoot(compositionRoot);
                var registrator = _registratorProvider.Provide(compositionRootAssembly);
                compositionRoot.Compose(registrator);
            }
        }

        private static Assembly GetCompositionRoot(ICompositionRoot compositionRoot)
        {
            var defaultCompositionRoot = compositionRoot as DefaultCompositionRoot;
            return defaultCompositionRoot == null
                ? compositionRoot.GetType().Assembly
                : defaultCompositionRoot.TargetAssembly;
        }
    }
}