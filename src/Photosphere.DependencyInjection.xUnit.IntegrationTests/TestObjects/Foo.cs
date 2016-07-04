namespace Photosphere.DependencyInjection.xUnit.IntegrationTests.TestObjects
{
    internal class Foo : IFoo
    {
        public Foo(IBar bar)
        {
            Bar = bar;
        }

        public IBar Bar { get; }
    }
}