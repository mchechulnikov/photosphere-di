using Moq;
using Photosphere.DependencyInjection.Lifetimes;
using Photosphere.DependencyInjection.Registrations.Services;
using Photosphere.DependencyInjection.Registrations.Services.CompositionRoots;
using Photosphere.DependencyInjection.UnitTests.TestObjects.CompositionRoots;
using Photosphere.DependencyInjection.UnitTests.TestObjects.Objects;
using Photosphere.DependencyInjection.UnitTests.TestUtils.Extensions;
using Xunit;

namespace Photosphere.DependencyInjection.UnitTests.Registrations
{
    public class RegistryInitializerTests
    {
        [Fact]
        internal void Initialize_AllCompositionRootsComposed_Success()
        {
            var isFooRegistered = false;
            var isBarRegistered = false;
            var compositionRootProvider = new Mock<ICompositionRootProvider>().GetInstance(mock =>
            {
                mock.Setup(p => p.Provide()).Returns(new ICompositionRoot[] { new FirstCompositionRoot(), new SecondCompositionRoot() });
            });
            var registrator = new Mock<IRegistrator>().GetInstance(mock =>
            {
                mock.Setup(p => p.Register<IFoo>(Lifetime.PerRequest)).Returns(mock.Object).Callback(() => isFooRegistered = true);
                mock.Setup(p => p.Register<IBar>(Lifetime.PerRequest)).Returns(mock.Object).Callback(() => isBarRegistered = true);
            });
            var registryInitializer = new RegistryInitializer(compositionRootProvider, registrator);

            registryInitializer.Initialize();

            Assert.True(isFooRegistered && isBarRegistered);
        }
    }
}