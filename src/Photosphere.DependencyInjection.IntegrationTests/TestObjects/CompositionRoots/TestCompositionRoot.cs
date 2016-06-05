using Photosphere.DependencyInjection.IntegrationTests.TestObjects.Objects;
using Photosphere.DependencyInjection.Lifetimes;

namespace Photosphere.DependencyInjection.IntegrationTests.TestObjects.CompositionRoots
{
    internal class TestCompositionRoot : ICompositionRoot
    {
        public void Compose(IRegistrator registrator)
        {
            registrator
                .Register<IPerRequestFoo>()
                .Register<IPerRequestBar>()
                .Register<IWithPerRequestDependencies>()
                .Register<IAlwaysNewFoo>(Lifetime.AlwaysNew)
                .Register<IAlwaysNewBar>(Lifetime.AlwaysNew)
                .Register<IWithAlwaysNewDependencies>();
        }
    }
}