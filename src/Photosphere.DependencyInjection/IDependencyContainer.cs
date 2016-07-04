using System;
using System.Collections.Generic;

namespace Photosphere.DependencyInjection
{
    public interface IDependencyContainer : IDisposable
    {
        T GetInstance<T>();

        object GetInstance(Type type);

        IEnumerable<TService> GetAllInstances<TService>();
    }
}