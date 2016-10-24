using Photosphere.DependencyInjection.Initialization.Registrations.ValueObjects;
using Photosphere.DependencyInjection.Initialization.Saturation.Generation.MethodBodyGenerating.SomeServices.GeneratingStrategies;

namespace Photosphere.DependencyInjection.Initialization.Saturation.Generation.MethodBodyGenerating.SomeServices
{
    internal interface IGeneratingStrategyProvider
    {
        IGeneratingStrategy Provide(IRegistration registration);
    }
}