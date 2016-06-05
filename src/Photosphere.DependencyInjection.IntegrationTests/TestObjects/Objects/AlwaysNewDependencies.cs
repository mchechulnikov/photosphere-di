namespace Photosphere.DependencyInjection.IntegrationTests.TestObjects.Objects
{
    internal class AlwaysNewDependencies : IAlwaysNewDependencies
    {
        public AlwaysNewDependencies(IAlwaysNewFoo foo, IAlwaysNewBar bar)
        {
            Foo = foo;
            Bar = bar;
        }

        public IAlwaysNewFoo Foo { get; }
        public IAlwaysNewBar Bar { get; }
    }
}