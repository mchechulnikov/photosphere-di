namespace Photosphere.DependencyInjection.Generation.MethodBodyGenerating
{
    internal class InstanceProvidingMethodBodyGenerator : IInstanceProvidingMethodBodyGenerator
    {
        public void Generate(GeneratingDesign design)
        {
            var resultVariable = design.ObjectGraph.GeneratingStrategy.Generate(design);
            design.Designer.ReturnStatement(resultVariable);
        }
    }
}