using System.Reflection;

namespace Photosphere.DependencyInjection.Initialization.Registrations
{
    internal interface IRegistratorProvider
    {
        IRegistrator Provide(Assembly assembly);
    }
}