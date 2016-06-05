namespace Photosphere.DependencyInjection.IntegrationTests.TestObjects.Objects
{
    internal class PerRequestDependencies : IPerRequestDependencies
    {
        public PerRequestDependencies(
            IPerRequestFoo foo,
            IPerRequestBar bar)
        {
            Foo = foo;
            Bar = bar;
        }

        public IPerRequestFoo Foo { get; }
        public IPerRequestBar Bar { get; }
    }
}