using Photosphere.DependencyInjection;
using TestAssembly.RootAttributeRegistration.Objects;

namespace TestAssembly.RootAttributeRegistration
{
    public class RootAttributeRegistrationTestCompositionRoot : ICompositionRoot
    {
        public void Compose(IRegistrator registrator)
        {
            registrator.Register<CompositionRootAttributeRegistrationService>();
        }
    }
}