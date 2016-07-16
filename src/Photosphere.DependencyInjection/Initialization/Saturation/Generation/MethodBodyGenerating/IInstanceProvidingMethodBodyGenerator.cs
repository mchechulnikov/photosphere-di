using Photosphere.DependencyInjection.Initialization.Saturation.Generation.MethodBodyGenerating.ValueObjects;

namespace Photosphere.DependencyInjection.Initialization.Saturation.Generation.MethodBodyGenerating
{
    internal interface IInstanceProvidingMethodBodyGenerator
    {
        void Generate(GeneratingDesign design);
    }
}