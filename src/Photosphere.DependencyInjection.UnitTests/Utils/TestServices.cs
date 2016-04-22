namespace Photosphere.DependencyInjection.UnitTests.Utils
{
    internal interface IFoo {}

    internal class Foo : IFoo {}

    internal class Bar
    {
        public Bar() {}
    }

    internal class Qiz
    {
        private readonly Bar _bar;

        public Qiz(Bar bar)
        {
            _bar = bar;
        }
    }
}