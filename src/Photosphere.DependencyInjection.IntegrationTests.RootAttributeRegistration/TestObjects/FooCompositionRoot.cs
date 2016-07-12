namespace Photosphere.DependencyInjection.IntegrationTests.RootAttributeRegistration.TestObjects
{
    internal class FooCompositionRoot : ICompositionRoot
    {
        public void Compose(IRegistrator registrator)
        {
            registrator.Register<Foo>();
        }
    }
}