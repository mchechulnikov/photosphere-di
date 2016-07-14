using System.Reflection;

namespace Photosphere.DependencyInjection.Registrations.Services
{
    internal interface IRegistratorProvider
    {
        IRegistrator Provide(Assembly assembly);
    }
}