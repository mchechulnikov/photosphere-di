using System.Reflection.Emit;

namespace Photosphere.DependencyInjection.Generators.MethodBodyGenerating
{
    internal class InstantiateMethodBodyGenerator : IInstantiateMethodBodyGenerator
    {
        public void Generate(GeneratingDesign design)
        {
            var resultVariable = GenerateForGraph(design);
            design.Designer.ReturnStatement(resultVariable);
        }

        private static LocalBuilder GenerateForGraph(GeneratingDesign design)
        {
            return design.ObjectGraph.GeneratingStrategy.Generate(design);
        }
    }
}