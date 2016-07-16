using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Photosphere.DependencyInjection.LifetimeManagement.Scopes;

namespace Photosphere.DependencyInjection.LifetimeManagement
{
    internal class ScopeKeeper : IScopeKeeper
    {
        private readonly IDictionary<int, IPerRequestScope> _perRequestScopes;

        public ScopeKeeper()
        {
            PerContainerScope = new PerContainerScope();
            _perRequestScopes = new ConcurrentDictionary<int, IPerRequestScope>();
        }
        private static int CurrentTaskId
        {
            get
            {
                if (Task.CurrentId == null)
                {
                    throw new InvalidOperationException($"Get/set per container scope possible into task only");
                }
                return Task.CurrentId.Value;
            }
        }

        public IPerContainerScope PerContainerScope { get; }

        public IPerRequestScope PerRequestScope => _perRequestScopes[CurrentTaskId];

        public void StartNewPerRequestScope()
        {
            _perRequestScopes.Add(CurrentTaskId, new PerRequestScope());
        }

        public void Dispose()
        {
            PerContainerScope.Dispose();
            foreach (var perRequestScope in _perRequestScopes.Values)
            {
                perRequestScope.Dispose();
            }
        }
    }
}