using System.Linq;
using System.Reflection.Emit;

namespace Photosphere.DependencyInjection.Generators.MethodBodyGenerating.Strategies
{
    internal class EnumerableProvidingGeneratingStrategy : IEnumerableProvidingGeneratingStrategy
    {
        public LocalBuilder Generate(GeneratingDesign design)
        {
            return design.Designer
                .DeclareVariable(design.ObjectGraph.ReturnType)
                .AssignTo(v => GenerateInstantiating(design))
                .Variable;
        }

        private void GenerateInstantiating(GeneratingDesign design)
        {
            var parameters = design.ObjectGraph.Children.Select(og => og.GeneratingStrategy.Generate(new GeneratingDesign
            {
                Designer = design.Designer,
                ObjectGraph = og
            })).ToList();
            var elementType = design.ObjectGraph.ImplementationType.GetElementType();

            design.Designer
                .CreateNewArray(elementType, parameters.Count)
                .FillArray(parameters);
        }
    }
}