namespace Photosphere.DependencyInjection.IntegrationTests.TestObjects.Objects
{
    internal interface IPerRequestBar
    {
        IPerRequestFoo Foo { get; }
    }
}