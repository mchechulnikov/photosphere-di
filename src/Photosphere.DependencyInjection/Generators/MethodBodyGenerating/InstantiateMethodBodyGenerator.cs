namespace Photosphere.DependencyInjection.Generators.MethodBodyGenerating
{
    internal class InstantiateMethodBodyGenerator : IInstantiateMethodBodyGenerator
    {
        public void Generate(GeneratingDesign design)
        {
            var resultVariable = design.ObjectGraph.GeneratingStrategy.Generate(design);
            design.Designer.ReturnStatement(resultVariable);
        }
    }
}