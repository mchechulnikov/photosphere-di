namespace Photosphere.DependencyInjection.IntegrationTests.TestObjects.Objects
{
    internal class PerContainerDependencies : IPerContainerDependencies
    {
        public PerContainerDependencies(IPerContainerFoo perContainerFoo, IPerContainerBar perContainerBar)
        {
            PerContainerFoo = perContainerFoo;
            PerContainerBar = perContainerBar;
        }

        public IPerContainerFoo PerContainerFoo { get; }
        public IPerContainerBar PerContainerBar { get; }
    }
}