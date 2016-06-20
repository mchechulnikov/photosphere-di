namespace Photosphere.DependencyInjection.Generation.MethodBodyGenerating.Services.InstantiatingGenerators
{
    internal class ArrayInstantiatingGenerator : InstantiatingGeneratorBase, IArrayInstantiatingGenerator
    {
        public void Generate(GeneratingDesign design)
        {
            var parameters = GenerateForSubGraphs(design);
            var elementType = design.ObjectGraph.ImplementationType.GetElementType();

            design.Designer
                .CreateNewArray(elementType, parameters.Count)
                .FillArray(parameters);
        }
    }
}