using System.Reflection.Emit;

namespace Photosphere.DependencyInjection.Generation.MethodBodyGenerating.Services.GeneratingStrategies
{
    internal interface IGeneratingStrategy
    {
        LocalBuilder Generate(GeneratingDesign design);
    }
}