namespace Photosphere.DependencyInjection.IntegrationTests.TestObjects.Objects
{
    internal interface ITestServiceWithDependencies
    {
        IFoo Foo { get; }
        IBar Bar { get; }
    }
}