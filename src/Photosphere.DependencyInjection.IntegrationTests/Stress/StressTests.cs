using System.Diagnostics;
using Photosphere.DependencyInjection.TestAssembly.Stress;
using Xunit;
using Xunit.Abstractions;

namespace Photosphere.DependencyInjection.IntegrationTests.Stress
{
    public class StressTests : IntegrationTestsBase
    {
        private readonly ITestOutputHelper _outputHelper;

        public StressTests(ITestOutputHelper outputHelper) : base(typeof(IStressService))
        {
            _outputHelper = outputHelper;
        }

        [Fact]
        internal void RegistrationStressTests()
        {
            var watch = new Stopwatch();
            watch.Start();
            var container = NewContainer;
            watch.Stop();
            _outputHelper.WriteLine($"Time: {watch.ElapsedMilliseconds} ms");
        }

        [Fact]
        internal void ResolveStressTests()
        {
            var watch = new Stopwatch();
            var container = NewContainer;
            watch.Start();
            var services = container.GetAllInstances<IStressService>();
            watch.Stop();
            _outputHelper.WriteLine($"Time: {watch.ElapsedMilliseconds} ms");
        }
    }
}