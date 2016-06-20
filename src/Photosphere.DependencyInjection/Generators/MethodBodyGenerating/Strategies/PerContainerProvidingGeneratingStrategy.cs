using Photosphere.DependencyInjection.Lifetimes.Scopes.Services;

namespace Photosphere.DependencyInjection.Generators.MethodBodyGenerating.Strategies
{
    internal class PerContainerProvidingGeneratingStrategy : GeneratingStrategyBase, IPerContainerProvidingGeneratingStrategy
    {
        private readonly IScopeKeeper _scopeKeeper;
        private readonly IntantiationGeneratingStrategy _intantiationGeneratingStrategy;

        public PerContainerProvidingGeneratingStrategy(
            IScopeKeeper scopeKeeper,
            IntantiationGeneratingStrategy intantiationGeneratingStrategy)
        {
            _scopeKeeper = scopeKeeper;
            _intantiationGeneratingStrategy = intantiationGeneratingStrategy;
        }

        protected override void GenerateInstantiating(GeneratingDesign design)
        {
            var scope = _scopeKeeper.PerContainerScope;
            int instanceIndex;
            if (!scope.AvailableInstancesIndexes.TryGetValue(design.ObjectGraph.ImplementationType, out instanceIndex))
            {
                instanceIndex = scope.AvailableInstancesIndexes.Count;
                scope.AvailableInstancesIndexes.Add(design.ObjectGraph.ImplementationType, instanceIndex);
            }

            var instanceVariable = design.Designer.DeclareVariable<object>().Variable;

            design.Designer
                .LoadArgumentToStack(0)
                .LoadArrayRefElementTo(instanceVariable, instanceIndex)
                .If().IsNull(instanceVariable)
                .BeginBranch()
                    .Action(() => _intantiationGeneratingStrategy.GenerateNewInstance(design))
                    .PopFromStackTo(instanceVariable)
                    .LoadArgumentToStack(0)
                    .SetArrayRefElement(instanceIndex, instanceVariable)
                .EndBranch()
                .PushToStack(instanceVariable)
                .CastToClass(design.ObjectGraph.ImplementationType);
        }
    }
}