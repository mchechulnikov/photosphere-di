namespace Photosphere.DependencyInjection.Lifetimes.Scopes.Services
{
    internal class ScopeKeeper : IScopeKeeper
    {
        public ScopeKeeper()
        {
            PerContainerScope = new PerContainerScope();
        }

        public IPerContainerScope PerContainerScope { get; }

        public IPerRequestScope PerRequestScope { get; private set; }

        public void StartNewScope()
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