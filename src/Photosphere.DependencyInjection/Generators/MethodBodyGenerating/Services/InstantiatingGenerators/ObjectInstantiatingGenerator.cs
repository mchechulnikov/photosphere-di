using System.Linq;

namespace Photosphere.DependencyInjection.Generators.MethodBodyGenerating.Services.InstantiatingGenerators
{
    internal class ObjectInstantiatingGenerator : IObjectInstantiatingGenerator
    {
        public void Generate(GeneratingDesign design)
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