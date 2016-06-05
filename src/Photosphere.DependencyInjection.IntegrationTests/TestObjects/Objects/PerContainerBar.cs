namespace Photosphere.DependencyInjection.IntegrationTests.TestObjects.Objects
{
    internal class PerContainerBar : IPerContainerBar
    {
        public PerContainerBar(IPerContainerFoo foo)
        {
            Foo = foo;
        }

        public IPerContainerFoo Foo { get; }
    }
}