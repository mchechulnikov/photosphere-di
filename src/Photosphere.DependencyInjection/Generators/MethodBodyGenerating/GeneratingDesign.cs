using Photosphere.DependencyInjection.Generators.MethodBodyGenerating.Builders;
using Photosphere.DependencyInjection.Generators.ObjectGraphs.DataTransferObjects;

namespace Photosphere.DependencyInjection.Generators.MethodBodyGenerating
{
    internal class GeneratingDesign
    {
        public ControlFlowDesigner Designer { get; set; }

        public IObjectGraph ObjectGraph { get; set; }
    }
}