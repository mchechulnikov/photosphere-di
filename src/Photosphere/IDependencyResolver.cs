namespace Photosphere
{
    public interface IDependencyResolver
    {
        T GetInstance<T>();
    }
}