namespace Photosphere.DependencyInjection
{
    public interface ICompositionRoot
    {
        void Compose(IRegistrator registrator);
    }
}