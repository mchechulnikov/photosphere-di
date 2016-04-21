namespace Photosphere.DependencyInjection
{
    public interface IRegistrator
    {
        IRegistrator Register<TService>();
    }
}