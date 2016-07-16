using Photosphere.DependencyInjection.Initialization.Saturation.Generation.ObjectGraphs.Exceptions;
using Photosphere.DependencyInjection.TestAssembly.Cycles;
using Xunit;

namespace Photosphere.DependencyInjection.IntegrationTests.Analysis
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