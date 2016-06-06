using Photosphere.DependencyInjection.IntegrationTests.TestObjects.Objects;
using Photosphere.DependencyInjection.Lifetimes;

namespace Photosphere.DependencyInjection.IntegrationTests.TestObjects.CompositionRoots
{
    internal class MainTestCompositionRoot : ICompositionRoot
    {
        public void Compose(IRegistrator registrator)
        {
            registrator
                .Register<IPerRequestFoo>()
                .Register<IPerRequestBar>()
                .Register<IPerRequestDependencies>()

                .Register<IAlwaysNewFoo>(Lifetime.AlwaysNew)
                .Register<IAlwaysNewBar>(Lifetime.AlwaysNew)
                .Register<IAlwaysNewDependencies>(Lifetime.AlwaysNew)

                .Register<IPerContainerFoo>(Lifetime.PerContainer)
                /*.Register<IPerContainerBar>(Lifetime.PerContainer)
                .Register<IPerContainerDependencies>(Lifetime.PerContainer)*/;
        }
    }
}