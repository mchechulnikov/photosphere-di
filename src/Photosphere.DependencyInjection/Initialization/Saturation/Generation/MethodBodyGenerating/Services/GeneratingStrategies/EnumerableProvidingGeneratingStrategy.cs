using Photosphere.DependencyInjection.Initialization.Saturation.Generation.MethodBodyGenerating.Services.InstantiatingGenerators;
using Photosphere.DependencyInjection.Initialization.Saturation.Generation.MethodBodyGenerating.ValueObjects;

namespace Photosphere.DependencyInjection.Initialization.Saturation.Generation.MethodBodyGenerating.Services.GeneratingStrategies
{
    internal class EnumerableProvidingGeneratingStrategy : GeneratingStrategyBase, IEnumerableProvidingGeneratingStrategy
    {
        private readonly IArrayInstantiatingGenerator _arrayInstantiatingGenerator;

        public EnumerableProvidingGeneratingStrategy(IArrayInstantiatingGenerator arrayInstantiatingGenerator)
        {
            _arrayInstantiatingGenerator = arrayInstantiatingGenerator;
        }

        protected override void GenerateDependencyProviding(GeneratingDesign design)
        {
            _arrayInstantiatingGenerator.Generate(design);
        }
    }
}