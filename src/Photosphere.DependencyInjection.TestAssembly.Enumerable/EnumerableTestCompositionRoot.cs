using Photosphere.DependencyInjection.Lifetimes;
using Photosphere.DependencyInjection.TestAssembly.Enumerable.TestObjects;

namespace Photosphere.DependencyInjection.TestAssembly.Enumerable
{
    internal class EnumerableTestCompositionRoot : ICompositionRoot
    {
        public void Compose(IRegistrator registrator)
        {
            registrator
                .Register<IFoo>()
                .Register<IEnumerableDependencyFoo>()
                .Register<IReadOnlyCollectionDependencyFoo>(Lifetime.AlwaysNew);
        }
    }
}