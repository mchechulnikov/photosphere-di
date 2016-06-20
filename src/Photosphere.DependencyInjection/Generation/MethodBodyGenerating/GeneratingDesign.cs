using Photosphere.DependencyInjection.Generation.MethodBodyGenerating.Designers;
using Photosphere.DependencyInjection.Generation.ObjectGraphs.DataTransferObjects;

namespace Photosphere.DependencyInjection.Generation.MethodBodyGenerating
{
    internal struct GeneratingDesign
    {
        public ControlFlowDesigner Designer { get; set; }

        public IObjectGraph ObjectGraph { get; set; }
    }
}