using Photosphere.CilEmitting;
using Photosphere.Registration.Services;

namespace Photosphere
{
    public class DependencyResolver : IDependencyResolver
    {
        private readonly IRegistryInitializer _registryInitializer;

        public DependencyResolver()
        {
            _registryInitializer = InstantiateMethodGenerator.Generate<IRegistryInitializer>().Invoke();
        }

        public void Initialize()
        {
            _registryInitializer.Initialize();
        }

        public T GetInstance<T>()
        {
            throw new System.NotImplementedException();
        }
    }
}