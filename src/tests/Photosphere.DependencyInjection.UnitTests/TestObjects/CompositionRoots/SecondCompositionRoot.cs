using Photosphere.DependencyInjection.UnitTests.TestObjects.Objects;

namespace Photosphere.DependencyInjection.UnitTests.TestObjects.CompositionRoots
{
    internal class SecondCompositionRoot : ICompositionRoot
    {
        public void Compose(IRegistrator registrator)
        {
            registrator.Register<IBar>();
        }
    }
}