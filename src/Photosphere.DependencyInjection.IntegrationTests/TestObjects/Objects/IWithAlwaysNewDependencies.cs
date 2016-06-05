namespace Photosphere.DependencyInjection.IntegrationTests.TestObjects.Objects
{
    internal interface IWithAlwaysNewDependencies
    {
        IAlwaysNewFoo Foo { get; }
        IAlwaysNewBar Bar { get; }
    }
}