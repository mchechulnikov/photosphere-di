using Photosphere.DependencyInjection.IntegrationTests.CyclicDependencies.TestObjects.Objects;

namespace Photosphere.DependencyInjection.IntegrationTests.CyclicDependencies.TestObjects.CompositionRoots
{
    internal class WithCyclicDependenciesTestCompositionRoot : ICompositionRoot
    {
        public void Compose(IRegistrator registrator)
        {
            registrator
                .Register<IFoo>()
                .Register<IBar>()
                .Register<IQiz>();
        }
    }
}