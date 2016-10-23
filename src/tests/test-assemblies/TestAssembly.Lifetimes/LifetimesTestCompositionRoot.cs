using Photosphere.DependencyInjection;
using TestAssembly.Lifetimes.TestObjects;

namespace TestAssembly.Lifetimes
{
    public class LifetimesTestCompositionRoot : ICompositionRoot
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
                .Register<IPerContainerBar>(Lifetime.PerContainer)
                .Register<IPerContainerDependencies>(Lifetime.PerContainer)

                .Register<IMixedLifetimesPerRequestDependencies>()
                .Register<IMixedLifetimesPerContainerDependencies>(Lifetime.PerContainer);
        }
    }
}