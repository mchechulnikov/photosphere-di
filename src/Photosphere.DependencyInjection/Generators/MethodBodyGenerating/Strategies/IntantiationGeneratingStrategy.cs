using System.Linq;
using System.Reflection.Emit;

namespace Photosphere.DependencyInjection.Generators.MethodBodyGenerating.Strategies
{
    internal class IntantiationGeneratingStrategy : IIntantiationGeneratingStrategy
    {
        public LocalBuilder Generate(GeneratingDesign design)
        {
            return design.Designer
                .DeclareVariable(design.ObjectGraph.ReturnType)
                .AssignTo(v => GenerateInstantiating(design))
                .Variable;
        }

        private static void GenerateInstantiating(GeneratingDesign design)
        {
            var parameters = design.ObjectGraph.Children.Select(og => og.GeneratingStrategy.Generate(new GeneratingDesign
            {
                Designer = design.Designer,
                ObjectGraph = og
            })).ToList();
            design.Designer.CreateNewObject(design.ObjectGraph.Constructor, parameters);
        }
    }
}