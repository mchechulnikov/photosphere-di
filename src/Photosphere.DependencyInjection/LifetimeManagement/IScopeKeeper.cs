using System;
using Photosphere.DependencyInjection.LifetimeManagement.Scopes;

namespace Photosphere.DependencyInjection.LifetimeManagement
{
    internal interface IScopeKeeper : IDisposable
    {
        IPerContainerScope PerContainerScope { get; }

        IPerRequestScope PerRequestScope { get; }

        void StartNewPerRequestScope();
    }
}