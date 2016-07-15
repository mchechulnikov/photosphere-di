using Photosphere.DependencyInjection.TestAssembly.RootAttributeRegistration.Objects;

namespace Photosphere.DependencyInjection.TestAssembly.RootAttributeRegistration
{
    public class RootAttributeRegistrationTestCompositionRoot : ICompositionRoot
    {
        public void Compose(IRegistrator registrator)
        {
            registrator.Register<CompositionRootAttributeRegistrationService>();
        }
    }
}