using Photosphere.DependencyInjection.Generation.MethodBodyGenerating.Services.InstantiatingGenerators;
using Photosphere.DependencyInjection.Generation.MethodBodyGenerating.ValueObjects;

namespace Photosphere.DependencyInjection.Generation.MethodBodyGenerating.Services.GeneratingStrategies
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