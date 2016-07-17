namespace Photosphere.DependencyInjection.Interception.Context.Services
{
    internal interface IMethodContextProvider
    {
        IMethodInvocationContext Get();
    }
}