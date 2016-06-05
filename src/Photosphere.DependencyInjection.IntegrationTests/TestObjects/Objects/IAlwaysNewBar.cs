namespace Photosphere.DependencyInjection.IntegrationTests.TestObjects.Objects
{
    internal interface IAlwaysNewBar
    {
        IAlwaysNewFoo Foo { get; }
    }
}