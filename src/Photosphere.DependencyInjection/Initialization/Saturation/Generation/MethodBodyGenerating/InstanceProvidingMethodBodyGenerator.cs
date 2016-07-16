using Photosphere.DependencyInjection.Initialization.Saturation.Generation.MethodBodyGenerating.ValueObjects;

namespace Photosphere.DependencyInjection.Initialization.Saturation.Generation.MethodBodyGenerating
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