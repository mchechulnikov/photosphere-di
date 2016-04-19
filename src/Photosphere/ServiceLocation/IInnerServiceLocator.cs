namespace Photosphere.ServiceLocation
{
    internal interface IInnerServiceLocator
    {
        TService GetInstance<TService>();
    }
}