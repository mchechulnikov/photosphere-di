namespace Photosphere.DependencyInjection
{
    public interface IDependencyResolver
    {
        void Initialize();

        T GetInstance<T>();
    }
}