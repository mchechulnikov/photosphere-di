using System.Collections.Generic;
using System.Linq;
using Photosphere.DependencyInjection.TestAssembly.Enumerable.TestObjects;
using Photosphere.DependencyInjection.TestAssembly.Enumerable2;
using Xunit;

namespace Photosphere.DependencyInjection.IntegrationTests.Resolve
{
    public class GetInstanceResolveEnumerableCrossAssemblyTests : IntegrationTestsBase
    {
        protected override IDependencyContainer NewContainer =>
            new DependencyContainer(typeof(IBuz1).Assembly, typeof(IBuz3).Assembly);

        [Fact]
        internal void GetInstance_EnumerableByThirdLevelInterfaceCrossAssembly_ExpectedInstancesTypes()
        {
            var result = NewContainer.GetInstance<IEnumerable<IBuz>>().ToList();

            Assert.Contains(typeof(Buz1), result.Select(x => x.GetType()));
            Assert.Contains(typeof(Buz2), result.Select(x => x.GetType()));
            Assert.Contains(typeof(Buz3), result.Select(x => x.GetType()));
            Assert.Contains(typeof(Buz4), result.Select(x => x.GetType()));
        }
    }
}