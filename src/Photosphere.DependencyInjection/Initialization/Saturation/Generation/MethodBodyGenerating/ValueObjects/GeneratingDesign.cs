using Photosphere.DependencyInjection.Initialization.Saturation.Generation.MethodBodyGenerating.Designers;
using Photosphere.DependencyInjection.Initialization.Saturation.Generation.ObjectGraphs.DataTransferObjects;

namespace Photosphere.DependencyInjection.Initialization.Saturation.Generation.MethodBodyGenerating.ValueObjects
{
    internal struct GeneratingDesign
    {
        public ControlFlowDesigner Designer { get; set; }

        public IObjectGraph ObjectGraph { get; set; }
    }
}