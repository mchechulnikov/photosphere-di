using System.Reflection;
using Photosphere.DependencyInjection.TestAssembly.Lifetimes.TestObjects;
using Xunit;

namespace Photosphere.DependencyInjection.IntegrationTests.Tests.GetInstace
{
    public class GetInstanceRegisteringByClassTests
    {
        private readonly Assembly _targetAssembly = typeof(AlwaysNewFoo).Assembly;

        [Fact]
        internal void GetInstance_ByClass_NotNull()
        {
            var contaner = new DependencyContainer(_targetAssembly);

            var foo = contaner.GetInstance<AlwaysNewFoo>();

            Assert.NotNull(foo);
        }
    }
}