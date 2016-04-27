using Photosphere.DependencyInjection.UnitTests.TestObjects.Objects;

namespace Photosphere.DependencyInjection.UnitTests.TestObjects.CompositionRoots
{
    internal class FirstCompositionRoot : ICompositionRoot
    {
        public void Compose(IRegistrator registrator)
        {
            registrator.Register<IFoo>();
        }
    }
}