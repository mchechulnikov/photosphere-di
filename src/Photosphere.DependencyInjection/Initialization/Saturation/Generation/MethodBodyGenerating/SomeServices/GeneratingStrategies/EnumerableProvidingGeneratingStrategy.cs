using Photosphere.DependencyInjection.Initialization.Saturation.Generation.MethodBodyGenerating.SomeServices.InstantiatingGenerators;
using Photosphere.DependencyInjection.Initialization.Saturation.Generation.MethodBodyGenerating.ValueObjects;

namespace Photosphere.DependencyInjection.Initialization.Saturation.Generation.MethodBodyGenerating.SomeServices.GeneratingStrategies
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