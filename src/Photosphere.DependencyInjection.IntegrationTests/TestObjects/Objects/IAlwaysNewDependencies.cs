namespace Photosphere.DependencyInjection.IntegrationTests.TestObjects.Objects
{
    internal interface IAlwaysNewDependencies
    {
        IAlwaysNewFoo Foo { get; }
        IAlwaysNewBar Bar { get; }
    }
}