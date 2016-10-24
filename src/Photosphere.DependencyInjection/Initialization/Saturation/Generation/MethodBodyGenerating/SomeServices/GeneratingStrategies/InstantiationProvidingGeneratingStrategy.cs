using Photosphere.DependencyInjection.Initialization.Saturation.Generation.MethodBodyGenerating.SomeServices.InstantiatingGenerators;
using Photosphere.DependencyInjection.Initialization.Saturation.Generation.MethodBodyGenerating.ValueObjects;

namespace Photosphere.DependencyInjection.Initialization.Saturation.Generation.MethodBodyGenerating.SomeServices.GeneratingStrategies
{
    internal class InstantiationProvidingGeneratingStrategy : GeneratingStrategyBase, IInstantiationProvidingGeneratingStrategy
    {
        private readonly IObjectInstantiatingGenerator _objectInstantiatingGenerator;

        public InstantiationProvidingGeneratingStrategy(IObjectInstantiatingGenerator objectInstantiatingGenerator)
        {
            _objectInstantiatingGenerator = objectInstantiatingGenerator;
        }

        protected override void GenerateDependencyProviding(GeneratingDesign design)
        {
            _objectInstantiatingGenerator.Generate(design);
        }
    }
}