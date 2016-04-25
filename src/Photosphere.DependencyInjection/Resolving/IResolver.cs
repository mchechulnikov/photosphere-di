namespace Photosphere.DependencyInjection.Resolving
{
    internal interface IResolver
    {
        TService GetInstance<TService>();
    }
}