namespace Photosphere.DependencyInjection.IntegrationTests.TestObjects.Objects
{
    internal interface IPerContainerDependencies
    {
        IPerContainerFoo PerContainerFoo { get; }
        IPerContainerBar PerContainerBar { get; }
    }
}