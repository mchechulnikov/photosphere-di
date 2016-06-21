using Photosphere.DependencyInjection.Generation.MethodBodyGenerating.ValueObjects;

namespace Photosphere.DependencyInjection.Generation.MethodBodyGenerating.Services.InstantiatingGenerators
{
    internal class ObjectInstantiatingGenerator : InstantiatingGeneratorBase, IObjectInstantiatingGenerator
    {
        public void Generate(GeneratingDesign design)
        {
            var parameters = GenerateForSubGraphs(design);
            design.Designer.CreateNewObject(design.ObjectGraph.Constructor, parameters);
        }
    }
}