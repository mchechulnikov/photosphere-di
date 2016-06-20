using Photosphere.DependencyInjection.Generators.MethodBodyGenerating.Services.InstantiatingGenerators;

namespace Photosphere.DependencyInjection.Generators.MethodBodyGenerating.Services.Strategies
{
    internal class IntantiationProvidingGeneratingStrategy : GeneratingStrategyBase, IIntantiationProvidingGeneratingStrategy
    {
        private readonly IObjectInstantiatingGenerator _objectInstantiatingGenerator;

        public IntantiationProvidingGeneratingStrategy(IObjectInstantiatingGenerator objectInstantiatingGenerator)
        {
            _objectInstantiatingGenerator = objectInstantiatingGenerator;
        }

        protected override void GenerateDependencyProviding(GeneratingDesign design)
        {
            _objectInstantiatingGenerator.Generate(design);
        }
    }
}