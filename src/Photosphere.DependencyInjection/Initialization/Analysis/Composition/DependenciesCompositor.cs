using System.Reflection;
using Photosphere.DependencyInjection.Extensions;
using Photosphere.DependencyInjection.Initialization.Analysis.Composition.CompositionRoots;
using Photosphere.DependencyInjection.Initialization.Analysis.Composition.CompositionRoots.ServiceCompositionRoots;
using Photosphere.DependencyInjection.Initialization.Registrations;

namespace Photosphere.DependencyInjection.Initialization.Analysis.Composition
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
            var compositionRoots = _compositionRootProvider.Provide();
            compositionRoots.ParallelProceed(compositionRoots.Count, Compose);
        }

        private void Compose(ICompositionRoot compositionRoot)
        {
            var compositionRootAssembly = GetCompositionRoot(compositionRoot);
            var registrator = _registratorProvider.Provide(compositionRootAssembly);
            compositionRoot.Compose(registrator);
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