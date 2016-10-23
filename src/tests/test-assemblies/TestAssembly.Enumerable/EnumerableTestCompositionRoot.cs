using Photosphere.DependencyInjection;
using TestAssembly.Enumerable.TestObjects;

namespace TestAssembly.Enumerable
{
    internal class EnumerableTestCompositionRoot : ICompositionRoot
    {
        public void Compose(IRegistrator registrator)
        {
            registrator
                .Register<IFoo>(Lifetime.PerContainer)
                .Register<IBar>(Lifetime.PerContainer)
                .Register<IBuzzy>(Lifetime.PerContainer)
                .Register<IEnumerableDependencyFoo>()
                .Register<IReadOnlyCollectionDependencyFoo>(Lifetime.PerContainer);
        }
    }
}