namespace Photosphere
{
    public interface IRegistrator
    {
        IRegistrator Register<TService, TImplementation>();

        IRegistrator Register<TService>();
    }
}