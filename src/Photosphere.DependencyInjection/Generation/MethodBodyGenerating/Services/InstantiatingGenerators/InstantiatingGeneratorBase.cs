using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using Photosphere.DependencyInjection.Generation.MethodBodyGenerating.ValueObjects;

namespace Photosphere.DependencyInjection.Generation.MethodBodyGenerating.Services.InstantiatingGenerators
{
    internal abstract class InstantiatingGeneratorBase
    {
        protected static IReadOnlyList<LocalBuilder> GenerateForSubGraphs(GeneratingDesign design)
        {
            return design.ObjectGraph.Children.Select(objectGraph => objectGraph.GeneratingStrategy.Generate(new GeneratingDesign
            {
                Designer = design.Designer,
                ObjectGraph = objectGraph
            })).ToList();
        }
    }
}