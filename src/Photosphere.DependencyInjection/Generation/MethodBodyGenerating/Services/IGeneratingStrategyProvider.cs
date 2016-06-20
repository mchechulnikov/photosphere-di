using Photosphere.DependencyInjection.Generation.MethodBodyGenerating.Services.GeneratingStrategies;
using Photosphere.DependencyInjection.Registrations.ValueObjects;

namespace Photosphere.DependencyInjection.Generation.MethodBodyGenerating.Services
{
    internal interface IGeneratingStrategyProvider
    {
        IGeneratingStrategy Provide(IRegistration registration);
    }
}