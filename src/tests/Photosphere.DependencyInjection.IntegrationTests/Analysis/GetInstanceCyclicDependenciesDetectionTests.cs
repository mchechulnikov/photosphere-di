using Photosphere.DependencyInjection.Initialization.Saturation.Generation.ObjectGraphs.Exceptions;
using TestAssembly.Cycles;
using Xunit;

namespace Photosphere.Di.IntegrationTests.Analysis
{
    public class GetInstanceCyclicDependenciesDetectionTests : IntegrationTestsBase
    {
        public GetInstanceCyclicDependenciesDetectionTests()
            : base(typeof(CyclesTestCompositionRoot)) {}

        [Fact]
        internal void GetInstance_CyclicDependenciesOnTwoDeepLevel_Exception()
        {
            Assert.Throws<DetectedCycleDependencyException>(() => NewContainer);
        }
    }
}