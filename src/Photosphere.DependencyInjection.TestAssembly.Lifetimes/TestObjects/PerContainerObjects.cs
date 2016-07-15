namespace Photosphere.DependencyInjection.TestAssembly.Lifetimes.TestObjects
{
    internal interface IPerContainerFoo {}
    internal class PerContainerFoo : IPerContainerFoo {}

    internal interface IPerContainerBar
    {
        IPerContainerFoo Foo { get; }
    }
    internal class PerContainerBar : IPerContainerBar
    {
        public PerContainerBar(IPerContainerFoo foo)
        {
            Foo = foo;
        }

        public IPerContainerFoo Foo { get; }
    }

    internal interface IPerContainerDependencies
    {
        IPerContainerFoo PerContainerFoo { get; }
        IPerContainerBar PerContainerBar { get; }
    }
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