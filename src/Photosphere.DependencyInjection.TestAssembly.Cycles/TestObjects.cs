namespace Photosphere.DependencyInjection.TestAssembly.Cycles
{
    internal interface IBar {}
    internal interface IFoo {}
    internal interface IQiz {}

    internal class Bar : IBar
    {
        public Bar(IQiz qiz) {}
    }

    internal class Foo : IFoo
    {
        public Foo(IBar bar) {}
    }

    internal class Qiz : IQiz
    {
        public Qiz(IFoo foo) {}
    }
}