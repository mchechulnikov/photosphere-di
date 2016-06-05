namespace Photosphere.DependencyInjection.IntegrationTests.TestObjects.Objects
{
    internal class AlwaysNewBar : IAlwaysNewBar
    {

        public AlwaysNewBar(IAlwaysNewFoo foo)
        {
            Foo = foo;
        }

        public IAlwaysNewFoo Foo { get; }
    }
}