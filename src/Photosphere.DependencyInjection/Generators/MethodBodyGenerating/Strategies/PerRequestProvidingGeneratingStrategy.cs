using System.Reflection.Emit;
using Photosphere.DependencyInjection.Lifetimes.Scopes.Services;

namespace Photosphere.DependencyInjection.Generators.MethodBodyGenerating.Strategies
{
    internal class PerRequestProvidingGeneratingStrategy : GeneratingStrategyBase, IPerRequestProvidingGeneratingStrategy
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

        protected override void GenerateInstantiating(GeneratingDesign generatingDesign)
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