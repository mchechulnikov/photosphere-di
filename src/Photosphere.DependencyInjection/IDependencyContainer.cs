using System;

namespace Photosphere.DependencyInjection
{
    public interface IDependencyContainer : IDisposable
    {
        void Initialize();

        T GetInstance<T>();
    }
}