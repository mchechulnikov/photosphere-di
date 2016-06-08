using System;

namespace Photosphere.DependencyInjection.Lifetimes.Scopes.Services
{
    internal interface IScopeKeeper : IDisposable
    {
        IPerContainerScope PerContainerScope { get; }

        IPerRequestScope PerRequestScope { get; }

        void StartNewPerRequestScope();
    }
}