using Photosphere.DependencyInjection.Generators.MethodBodyGenerating.Services.InstantiatingGenerators;

namespace Photosphere.DependencyInjection.Generators.MethodBodyGenerating.Services.Strategies
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