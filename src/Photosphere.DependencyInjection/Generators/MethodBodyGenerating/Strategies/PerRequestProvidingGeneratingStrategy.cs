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

        protected override void GenerateInstantiating(GeneratingDesign design)
        {
            var scope = _scopeKeeper.PerRequestScope;
            LocalBuilder instanceVariable;
            if (!scope.AvailableInstancesVariables.TryGetValue(design.ObjectGraph.ImplementationType, out instanceVariable))
            {
                instanceVariable = design.Designer
                    .DeclareVariable(design.ObjectGraph.ImplementationType)
                    .AssignTo(v =>
                    {
                        scope.Add(design.ObjectGraph.ImplementationType, v);
                        _intantiationGeneratingStrategy.GenerateNewInstance(design);
                    })
                    .Variable;
            }
            design.Designer.PushToStack(instanceVariable);
        }
    }
}