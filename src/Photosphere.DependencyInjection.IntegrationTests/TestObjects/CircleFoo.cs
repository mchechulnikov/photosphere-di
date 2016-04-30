namespace Photosphere.DependencyInjection.IntegrationTests.TestObjects
{
    internal class CircleFoo: ICircleFoo
    {
        private readonly ICircleBar _bar;

        public CircleFoo(ICircleBar bar)
        {
            _bar = bar;
        }
    }
}