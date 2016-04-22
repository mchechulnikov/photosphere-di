namespace Photosphere.DependencyInjection.TestUtils
{
    public interface IFoo {}
    public interface IBar {}
    public interface IQiz {}

    public class Foo : IFoo {}

    public class Bar : IBar
    {
        public Bar() {}
    }

    public class Qiz : IQiz
    {
        private readonly IBar _bar;

        public Qiz(Bar bar)
        {
            _bar = bar;
        }
    }
}