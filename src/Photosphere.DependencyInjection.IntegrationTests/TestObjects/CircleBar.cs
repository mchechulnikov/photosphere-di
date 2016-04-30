namespace Photosphere.DependencyInjection.IntegrationTests.TestObjects
{
    internal class CircleBar : ICircleBar
    {
        private readonly ICircleFoo _foo;

        public CircleBar(ICircleFoo foo)
        {
            _foo = foo;
        }
    }
}