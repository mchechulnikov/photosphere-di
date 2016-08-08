using Photosphere.DependencyInjection.TestAssembly.Enumerable.TestObjects;

namespace Photosphere.DependencyInjection.TestAssembly.Enumerable
{
    internal class EnumerableTestCompositionRoot : ICompositionRoot
    {
        public void Compose(IRegistrator registrator)
        {
            registrator
                .Register<IFoo>(Lifetime.PerContainer)
                .Register<IBar>(Lifetime.PerContainer)
                .Register<IBuz>(Lifetime.PerContainer)
                .Register<IEnumerableDependencyFoo>()
                .Register<IReadOnlyCollectionDependencyFoo>(Lifetime.PerContainer);
        }
    }
}