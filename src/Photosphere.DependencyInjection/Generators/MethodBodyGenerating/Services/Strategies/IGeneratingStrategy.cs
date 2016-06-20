using System.Reflection.Emit;

namespace Photosphere.DependencyInjection.Generators.MethodBodyGenerating.Services.Strategies
{
    internal interface IGeneratingStrategy
    {
        LocalBuilder Generate(GeneratingDesign design);
    }
}