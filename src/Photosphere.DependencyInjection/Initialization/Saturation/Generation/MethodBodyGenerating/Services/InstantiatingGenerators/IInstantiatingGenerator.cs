using Photosphere.DependencyInjection.Initialization.Saturation.Generation.MethodBodyGenerating.ValueObjects;

namespace Photosphere.DependencyInjection.Initialization.Saturation.Generation.MethodBodyGenerating.Services.InstantiatingGenerators
{
    internal interface IInstantiatingGenerator
    {
        void Generate(GeneratingDesign design);
    }
}