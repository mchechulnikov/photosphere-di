namespace TestAssembly.Lifetimes.TestObjects
{
    internal interface IAlwaysNewFoo { }
    internal class AlwaysNewFoo : IAlwaysNewFoo { }

    internal interface IAlwaysNewBar
    {
        IAlwaysNewFoo AlwaysNewFoo { get; }
        IPerRequestBar PerRequestBar { get; }
    }

    internal class AlwaysNewBar : IAlwaysNewBar
    {
        public AlwaysNewBar(IAlwaysNewFoo alwaysNewFoo, IPerRequestBar perRequestBar)
        {
            AlwaysNewFoo = alwaysNewFoo;
            PerRequestBar = perRequestBar;
        }

        public IAlwaysNewFoo AlwaysNewFoo { get; }
        public IPerRequestBar PerRequestBar { get; }
    }

    internal interface IAlwaysNewDependencies
    {
        IAlwaysNewFoo Foo { get; }
        IAlwaysNewBar Bar { get; }
    }

    internal class AlwaysNewDependencies : IAlwaysNewDependencies
    {
        public AlwaysNewDependencies(IAlwaysNewFoo foo, IAlwaysNewBar bar)
        {
            Foo = foo;
            Bar = bar;
        }

        public IAlwaysNewFoo Foo { get; }
        public IAlwaysNewBar Bar { get; }
    }
}