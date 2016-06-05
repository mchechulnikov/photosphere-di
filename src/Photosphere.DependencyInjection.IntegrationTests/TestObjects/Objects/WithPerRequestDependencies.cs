namespace Photosphere.DependencyInjection.IntegrationTests.TestObjects.Objects
{
    internal class WithPerRequestDependencies : IWithPerRequestDependencies
    {
        public WithPerRequestDependencies(
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