using System.Reflection.Emit;
using Photosphere.DependencyInjection.Generation.MethodBodyGenerating.ValueObjects;

namespace Photosphere.DependencyInjection.Generation.MethodBodyGenerating.Services.GeneratingStrategies
{
    internal interface IGeneratingStrategy
    {
        LocalBuilder Generate(GeneratingDesign design);
    }
}