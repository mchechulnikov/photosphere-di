using System.Reflection.Emit;
using Photosphere.DependencyInjection.Generation.MethodBodyGenerating.ValueObjects;

namespace Photosphere.DependencyInjection.Generation.MethodBodyGenerating.Services.GeneratingStrategies
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