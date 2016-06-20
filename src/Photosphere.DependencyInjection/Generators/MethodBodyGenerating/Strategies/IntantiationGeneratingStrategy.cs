using System.Linq;

namespace Photosphere.DependencyInjection.Generators.MethodBodyGenerating.Strategies
{
    internal class IntantiationGeneratingStrategy : GeneratingStrategyBase, IIntantiationGeneratingStrategy
    {
        protected override void GenerateInstantiating(GeneratingDesign design)
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