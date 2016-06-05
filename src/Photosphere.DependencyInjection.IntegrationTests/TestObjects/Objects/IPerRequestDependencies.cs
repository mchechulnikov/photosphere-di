namespace Photosphere.DependencyInjection.IntegrationTests.TestObjects.Objects
{
    internal interface IPerRequestDependencies
    {
        IPerRequestFoo Foo { get; }
        IPerRequestBar Bar { get; }
    }
}