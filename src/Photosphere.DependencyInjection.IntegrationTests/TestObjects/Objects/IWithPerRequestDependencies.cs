namespace Photosphere.DependencyInjection.IntegrationTests.TestObjects.Objects
{
    internal interface IWithPerRequestDependencies
    {
        IPerRequestFoo Foo { get; }
        IPerRequestBar Bar { get; }
    }
}