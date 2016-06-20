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

        protected override void GenerateInstantiating(GeneratingDesign generatingDesign)
        {
            var scope = _scopeKeeper.PerContainerScope;
            int instanceIndex;
            if (!scope.AvailableInstancesIndexes.TryGetValue(generatingDesign.ObjectGraph.ImplementationType, out instanceIndex))
            {
                instanceIndex = scope.AvailableInstancesIndexes.Count;
                scope.AvailableInstancesIndexes.Add(generatingDesign.ObjectGraph.ImplementationType, instanceIndex);
            }

            var instanceVariable = generatingDesign.Designer.DeclareVariable<object>().Variable;

            generatingDesign.Designer
                .LoadArgumentToStack(0)
                .LoadArrayRefElementTo(instanceVariable, instanceIndex)
                .If().IsNull(instanceVariable)
                .BeginBranch()
                .Action(() => _intantiationGeneratingStrategy.Generate(generatingDesign))
                .PopFromStackTo(instanceVariable)
                .LoadArgumentToStack(0)
                .SetArrayRefElement(instanceIndex, instanceVariable)
                .EndBranch()
                .PushToStack(instanceVariable)
                .CastToClass(generatingDesign.ObjectGraph.ImplementationType);
        }
    }
}