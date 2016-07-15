using System.Reflection;
using Photosphere.DependencyInjection.Generation.ObjectGraphs.Exceptions;
using Photosphere.DependencyInjection.TestAssembly.Cycles;
using Xunit;

namespace Photosphere.DependencyInjection.IntegrationTests.Tests
{
    public class CyclicDependenciesDetectionTests
    {
        private static readonly Assembly TargetAssembly = typeof(CyclesTestCompositionRoot).Assembly;

        [Fact]
        internal void GetInstance_CyclicDependenciesOnTwoDeepLevel_Exception()
        {
            Assert.Throws<DetectedCycleDependencyException>(() => new DependencyContainer(TargetAssembly));
        }
    }
}