using System;
using Photosphere.DependencyInjection.Initialization.Registrations.ValueObjects;
using Photosphere.DependencyInjection.Initialization.Saturation.Generation.MethodBodyGenerating.SomeServices.GeneratingStrategies;

namespace Photosphere.DependencyInjection.Initialization.Saturation.Generation.MethodBodyGenerating.SomeServices
{
    internal class GeneratingStrategyProvider : IGeneratingStrategyProvider
    {
        private readonly IInstantiationProvidingGeneratingStrategy _instantiationProvidingGeneratingStrategy;
        private readonly IPerRequestProvidingGeneratingStrategy _perRequestProvidingGeneratingStrategy;
        private readonly IPerContainerProvidingGeneratingStrategy _perContainerProvidingGeneratingStrategy;
        private readonly IEnumerableProvidingGeneratingStrategy _enumerableProvidingGeneratingStrategy;

        public GeneratingStrategyProvider(
            IInstantiationProvidingGeneratingStrategy instantiationProvidingGeneratingStrategy,
            IPerRequestProvidingGeneratingStrategy perRequestProvidingGeneratingStrategy,
            IPerContainerProvidingGeneratingStrategy perContainerProvidingGeneratingStrategy,
            IEnumerableProvidingGeneratingStrategy enumerableProvidingGeneratingStrategy)
        {
            _instantiationProvidingGeneratingStrategy = instantiationProvidingGeneratingStrategy;
            _perRequestProvidingGeneratingStrategy = perRequestProvidingGeneratingStrategy;
            _perContainerProvidingGeneratingStrategy = perContainerProvidingGeneratingStrategy;
            _enumerableProvidingGeneratingStrategy = enumerableProvidingGeneratingStrategy;
        }

        public IGeneratingStrategy Provide(IRegistration registration)
        {
            if (registration.IsEnumerable)
            {
                return _enumerableProvidingGeneratingStrategy;
            }
            switch (registration.Lifetime)
            {
                case Lifetime.AlwaysNew:
                    return _instantiationProvidingGeneratingStrategy;
                case Lifetime.PerRequest:
                    return _perRequestProvidingGeneratingStrategy;
                case Lifetime.PerContainer:
                    return _perContainerProvidingGeneratingStrategy;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}