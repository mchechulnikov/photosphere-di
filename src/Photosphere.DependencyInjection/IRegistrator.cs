namespace Photosphere
{
    public interface IRegistrator
    {
        IRegistrator Register<TService>();
    }
}