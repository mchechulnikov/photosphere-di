using Photosphere.Registration.Services;
using Photosphere.ServiceLocation;

namespace Photosphere
{
    public class DependencyResolver : IDependencyResolver
    {
        private static readonly IInnerServiceLocator ServiceLocator = new InnerServiceLocator();

        public DependencyResolver()
        {
            ServiceLocator.GetInstance<IRegistryInitializer>().Initialize();
        }

        public T GetInstance<T>()
        {
            throw new System.NotImplementedException();
        }
    }
}