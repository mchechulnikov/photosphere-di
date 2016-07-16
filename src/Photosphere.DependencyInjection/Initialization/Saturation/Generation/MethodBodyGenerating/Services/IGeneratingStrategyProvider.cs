using Photosphere.DependencyInjection.Initialization.Registrations.ValueObjects;
using Photosphere.DependencyInjection.Initialization.Saturation.Generation.MethodBodyGenerating.Services.GeneratingStrategies;

namespace Photosphere.DependencyInjection.Initialization.Saturation.Generation.MethodBodyGenerating.Services
{
    internal interface IGeneratingStrategyProvider
    {
        IGeneratingStrategy Provide(IRegistration registration);
    }
}