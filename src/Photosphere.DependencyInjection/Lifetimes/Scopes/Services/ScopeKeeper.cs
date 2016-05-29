using System;

namespace Photosphere.DependencyInjection.Lifetimes.Scopes.Services
{
    internal class ScopeKeeper : IScopeKeeper
    {
        private readonly IIntegratedScope _perContainerScope;
        private IIntegratedScope _perRequestScope;

        public ScopeKeeper()
        {
            _perContainerScope = new IntegratedScope();
        }

        public IIntegratedScope Provide(Lifetime lifetime)
        {
            switch (lifetime)
            {
                case Lifetime.PerContainer:
                    return _perContainerScope;
                case Lifetime.PerRequest:
                    return _perRequestScope;
                default:
                    throw new InvalidOperationException("Unexpected lifetime");
            }
        }

        public void StartNewScope()
        {
            _perRequestScope = new IntegratedScope();
        }

        public void Dispose()
        {
            _perContainerScope.Dispose();
            _perRequestScope.Dispose();
        }
    }
}