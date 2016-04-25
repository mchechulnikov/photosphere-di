namespace Photosphere.DependencyInjection
{
    public interface IDependencyContainer
    {
        void Initialize();

        T GetInstance<T>();
    }
}