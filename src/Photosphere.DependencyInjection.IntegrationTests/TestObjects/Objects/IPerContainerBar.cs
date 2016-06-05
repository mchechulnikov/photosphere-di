namespace Photosphere.DependencyInjection.IntegrationTests.TestObjects.Objects
{
    internal interface IPerContainerBar
    {
        IPerContainerFoo Foo { get; }
    }
}