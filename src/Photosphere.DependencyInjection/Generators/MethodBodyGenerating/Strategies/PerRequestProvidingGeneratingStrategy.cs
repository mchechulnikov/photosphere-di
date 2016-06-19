using System.Reflection.Emit;
using Photosphere.DependencyInjection.Lifetimes.Scopes.Services;

namespace Photosphere.DependencyInjection.Generators.MethodBodyGenerating.Strategies
{
    internal class PerRequestProvidingGeneratingStrategy : IPerRequestProvidingGeneratingStrategy
    {
        private readonly IScopeKeeper _scopeKeeper;
        private readonly IntantiationGeneratingStrategy _intantiationGeneratingStrategy;

        public PerRequestProvidingGeneratingStrategy(
            IScopeKeeper scopeKeeper,
            IntantiationGeneratingStrategy intantiationGeneratingStrategy)
        {
            _scopeKeeper = scopeKeeper;
            _intantiationGeneratingStrategy = intantiationGeneratingStrategy;
        }

        public LocalBuilder Generate(GeneratingDesign design)
        {
            return design.Designer
                .DeclareVariable(design.ObjectGraph.ReturnType)
                .AssignTo(v => GenerateInstantiating(design))
                .Variable;
        }

        private void GenerateInstantiating(GeneratingDesign generatingDesign)
        {
            var scope = _scopeKeeper.PerRequestScope;
            LocalBuilder instanceVariable;
            if (!scope.AvailableInstancesVariables.TryGetValue(generatingDesign.ObjectGraph.ImplementationType, out instanceVariable))
            {
                instanceVariable = generatingDesign.Designer
                    .DeclareVariable(generatingDesign.ObjectGraph.ImplementationType)
                    .AssignTo(v =>
                    {
                        scope.Add(generatingDesign.ObjectGraph.ImplementationType, v);
                        _intantiationGeneratingStrategy.Generate(generatingDesign);
                    })
                    .Variable;
            }
            generatingDesign.Designer.PushToStack(instanceVariable);
        }
    }
}