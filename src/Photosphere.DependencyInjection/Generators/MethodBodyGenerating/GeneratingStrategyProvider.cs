using System;
using Photosphere.DependencyInjection.Generators.MethodBodyGenerating.Strategies;
using Photosphere.DependencyInjection.Lifetimes;
using Photosphere.DependencyInjection.Registrations.ValueObjects;

namespace Photosphere.DependencyInjection.Generators.MethodBodyGenerating
{
    internal class GeneratingStrategyProvider : IGeneratingStrategyProvider
    {
        private readonly IIntantiationGeneratingStrategy _intantiationGeneratingStrategy;
        private readonly IPerRequestProvidingGeneratingStrategy _perRequestProvidingGeneratingStrategy;
        private readonly IPerContainerProvidingGeneratingStrategy _perContainerProvidingGeneratingStrategy;
        private readonly IEnumerableProvidingGeneratingStrategy _enumerableProvidingGeneratingStrategy;

        public GeneratingStrategyProvider(
            IIntantiationGeneratingStrategy intantiationGeneratingStrategy,
            IPerRequestProvidingGeneratingStrategy perRequestProvidingGeneratingStrategy,
            IPerContainerProvidingGeneratingStrategy perContainerProvidingGeneratingStrategy,
            IEnumerableProvidingGeneratingStrategy enumerableProvidingGeneratingStrategy)
        {
            _intantiationGeneratingStrategy = intantiationGeneratingStrategy;
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
                    return _intantiationGeneratingStrategy;
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