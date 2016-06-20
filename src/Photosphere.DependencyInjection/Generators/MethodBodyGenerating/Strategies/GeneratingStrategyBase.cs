using System.Reflection.Emit;

namespace Photosphere.DependencyInjection.Generators.MethodBodyGenerating.Strategies
{
    internal abstract class GeneratingStrategyBase : IGeneratingStrategy
    {
        public LocalBuilder Generate(GeneratingDesign design)
        {
            return design.Designer
                .DeclareVariable(design.ObjectGraph.ReturnType)
                .AssignTo(v => GenerateInstantiating(design))
                .Variable;
        }

        protected abstract void GenerateInstantiating(GeneratingDesign design);
    }
}