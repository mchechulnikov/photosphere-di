namespace Photosphere
{
    public interface IDependencyResolver
    {
        void Initialize();

        T GetInstance<T>();
    }
}