using System;

namespace Photosphere.DependencyInjection.Lifetimes.Scopes.Services
{
    internal interface IScopeKeeper : IDisposable
    {
        IIntegratedScope Provide(Lifetime lifetime);
        void StartNewScope();
    }
}