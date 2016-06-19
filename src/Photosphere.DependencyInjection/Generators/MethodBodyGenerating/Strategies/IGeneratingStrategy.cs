using System.Reflection.Emit;

namespace Photosphere.DependencyInjection.Generators.MethodBodyGenerating.Strategies
{
    internal interface IGeneratingStrategy
    {
        LocalBuilder Generate(GeneratingDesign design);
    }
}