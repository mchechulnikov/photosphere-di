using Photosphere.DependencyInjection.LifetimeManagement.Scopes;

namespace Photosphere.DependencyInjection.LifetimeManagement
{
    internal class ScopeKeeper : IScopeKeeper
    {
        public ScopeKeeper()
        {
            PerContainerScope = new PerContainerScope();
        }

        public IPerContainerScope PerContainerScope { get; }

        public IPerRequestScope PerRequestScope { get; private set; }

        public void StartNewPerRequestScope()
        {
            PerRequestScope = new PerRequestScope();
        }

        public void Dispose()
        {
            PerContainerScope.Dispose();
            PerRequestScope.Dispose();
        }
    }
}