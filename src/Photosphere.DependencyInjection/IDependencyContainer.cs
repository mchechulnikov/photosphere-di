using System;

namespace Photosphere.DependencyInjection
{
    public interface IDependencyContainer : IDisposable
    {
        T GetInstance<T>();
    }
}