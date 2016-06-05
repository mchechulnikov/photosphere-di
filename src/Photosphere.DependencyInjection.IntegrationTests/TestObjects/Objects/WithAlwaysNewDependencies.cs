namespace Photosphere.DependencyInjection.IntegrationTests.TestObjects.Objects
{
    internal class WithAlwaysNewDependencies : IWithAlwaysNewDependencies
    {
        public WithAlwaysNewDependencies(IAlwaysNewFoo foo, IAlwaysNewBar bar)
        {
            Foo = foo;
            Bar = bar;
        }

        public IAlwaysNewFoo Foo { get; }
        public IAlwaysNewBar Bar { get; }
    }
}