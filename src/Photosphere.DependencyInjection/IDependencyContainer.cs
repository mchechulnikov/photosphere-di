using System;
using System.Collections.Generic;

namespace Photosphere.DependencyInjection
{
    public interface IDependencyContainer : IDisposable
    {
        T GetInstance<T>();

        IEnumerable<TService> GetAllInstances<TService>();
    }
}