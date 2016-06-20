using Photosphere.DependencyInjection.Generators.MethodBodyGenerating.Services.Strategies;
using Photosphere.DependencyInjection.Registrations.ValueObjects;

namespace Photosphere.DependencyInjection.Generators.MethodBodyGenerating.Services
{
    internal interface IGeneratingStrategyProvider
    {
        IGeneratingStrategy Provide(IRegistration registration);
    }
}