using Photosphere.DependencyInjection.Generators.MethodBodyGenerating.Strategies;
using Photosphere.DependencyInjection.Registrations.ValueObjects;

namespace Photosphere.DependencyInjection.Generators.MethodBodyGenerating
{
    internal interface IGeneratingStrategyProvider
    {
        IGeneratingStrategy Provide(IRegistration registration);
    }
}