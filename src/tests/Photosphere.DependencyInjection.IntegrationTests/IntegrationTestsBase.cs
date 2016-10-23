using System;
using Photosphere.DependencyInjection;

namespace Photosphere.Di.IntegrationTests
{
    public abstract class IntegrationTestsBase
    {
        private readonly Type _typeFromTargetAssembly;

        protected internal IntegrationTestsBase() {}

        protected internal IntegrationTestsBase(Type typeFromTargetAssembly)
        {
            _typeFromTargetAssembly = typeFromTargetAssembly;
        }

        protected virtual IDependencyContainer NewContainer =>
            new DependencyContainer(_typeFromTargetAssembly.Assembly);
    }
}