using Photosphere.DependencyInjection.IntegrationTests.TestObjects.Objects;
using Photosphere.DependencyInjection.IntegrationTests.TestObjects.Objects.Generic;
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
                .Register<IPerContainerBar>(Lifetime.PerContainer)
                .Register<IPerContainerDependencies>(Lifetime.PerContainer)
                
                .Register<IMixedLifetimesPerRequestDependencies>()
                .Register<IMixedLifetimesPerContainerDependencies>(Lifetime.PerContainer)
                
                .Register<IService>()
                
                .Register<IEnumerableDependencyFoo>()
                .Register<IReadOnlyCollectionDependencyFoo>(Lifetime.AlwaysNew)
                
                .Register(typeof(IGenericService<>))

                .Register(typeof(GenericServiceClass<>));
        }
    }
}