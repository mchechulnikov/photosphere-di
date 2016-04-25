using Photosphere.DependencyInjection.Lifetimes;

namespace Photosphere.DependencyInjection
{
    public interface IRegistrator
    {
        IRegistrator Register<TService>(Lifetime lifetime = Lifetime.PerRequest);
    }
}