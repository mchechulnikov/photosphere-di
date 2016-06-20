using System;
using Photosphere.DependencyInjection.Generators.MethodBodyGenerating.Services.Strategies;
using Photosphere.DependencyInjection.Lifetimes;
using Photosphere.DependencyInjection.Registrations.ValueObjects;

namespace Photosphere.DependencyInjection.Generators.MethodBodyGenerating.Services
{
    internal class GeneratingStrategyProvider : IGeneratingStrategyProvider
    {
        private readonly IIntantiationProvidingGeneratingStrategy _intantiationProvidingGeneratingStrategy;
        private readonly IPerRequestProvidingGeneratingStrategy _perRequestProvidingGeneratingStrategy;
        private readonly IPerContainerProvidingGeneratingStrategy _perContainerProvidingGeneratingStrategy;
        private readonly IEnumerableProvidingGeneratingStrategy _enumerableProvidingGeneratingStrategy;

        public GeneratingStrategyProvider(
            IIntantiationProvidingGeneratingStrategy intantiationProvidingGeneratingStrategy,
            IPerRequestProvidingGeneratingStrategy perRequestProvidingGeneratingStrategy,
            IPerContainerProvidingGeneratingStrategy perContainerProvidingGeneratingStrategy,
            IEnumerableProvidingGeneratingStrategy enumerableProvidingGeneratingStrategy)
        {
            _intantiationProvidingGeneratingStrategy = intantiationProvidingGeneratingStrategy;
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
                    return _intantiationProvidingGeneratingStrategy;
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