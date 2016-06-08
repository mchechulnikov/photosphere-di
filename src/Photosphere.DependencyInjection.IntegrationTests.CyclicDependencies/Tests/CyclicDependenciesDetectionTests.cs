using Photosphere.DependencyInjection.Generators.ObjectGraphs.Exceptions;
using Xunit;

namespace Photosphere.DependencyInjection.IntegrationTests.CyclicDependencies.Tests
{
    public class CyclicDependenciesDetectionTests
    {
        [Fact]
        internal void GetInstance_CyclicDependenciesOnTwoDeepLevel_Exception()
        {
            Assert.Throws<DetectedCycleDependencyException>(() => new DependencyContainer());
        }
    }
}