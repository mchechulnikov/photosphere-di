using System.Reflection.Emit;
using Photosphere.DependencyInjection.Initialization.Saturation.Generation.MethodBodyGenerating.ValueObjects;

namespace Photosphere.DependencyInjection.Initialization.Saturation.Generation.MethodBodyGenerating.Services.GeneratingStrategies
{
    internal abstract class GeneratingStrategyBase : IGeneratingStrategy
    {
        public LocalBuilder Generate(GeneratingDesign design)
        {
            return design.Designer
                .DeclareVariable(design.ObjectGraph.ReturnType)
                .AssignTo(v => GenerateDependencyProviding(design))
                .Variable;
        }

        protected abstract void GenerateDependencyProviding(GeneratingDesign design);
    }
}