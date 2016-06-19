using System;
using Photosphere.DependencyInjection.Generators.MethodBodyGenerating.Strategies;
using Photosphere.DependencyInjection.Generators.ObjectGraphs.DataTransferObjects;
using Photosphere.DependencyInjection.Lifetimes;

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

        public IGeneratingStrategy Provide(IObjectGraph objectGraph)
        {
            if (objectGraph.IsEnumerable)
            {
                return _enumerableProvidingGeneratingStrategy;
            }
            switch (objectGraph.Lifetime)
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