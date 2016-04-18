using Photosphere.Registration;
using Photosphere.Registration.Services;

namespace Photosphere
{
    public class DependencyResolver : IDependencyResolver
    {
        public DependencyResolver()
        {
            new RegistryInitializer().Initialize();
        }

        public T GetInstance<T>()
        {
            throw new System.NotImplementedException();
        }
    }
}