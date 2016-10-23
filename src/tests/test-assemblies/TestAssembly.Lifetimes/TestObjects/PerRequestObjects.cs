namespace TestAssembly.Lifetimes.TestObjects
{
    internal interface IPerRequestFoo { }
    internal class PerRequestFoo : IPerRequestFoo { }

    internal interface IPerRequestBar
    {
        IPerRequestFoo PerRequestFoo { get; }
    }

    internal class PerRequestBar : IPerRequestBar
    {
        public PerRequestBar(IPerRequestFoo perRequestFoo)
        {
            PerRequestFoo = perRequestFoo;
        }

        public IPerRequestFoo PerRequestFoo { get; }
    }

    internal interface IPerRequestDependencies
    {
        IPerRequestFoo Foo { get; }
        IPerRequestBar Bar { get; }
    }

    internal class PerRequestDependencies : IPerRequestDependencies
    {
        public PerRequestDependencies(
            IPerRequestFoo foo,
            IPerRequestBar bar)
        {
            Foo = foo;
            Bar = bar;
        }

        public IPerRequestFoo Foo { get; }
        public IPerRequestBar Bar { get; }
    }
}