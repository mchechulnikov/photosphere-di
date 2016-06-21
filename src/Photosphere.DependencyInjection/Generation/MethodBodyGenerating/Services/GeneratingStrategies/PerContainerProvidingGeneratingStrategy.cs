using Photosphere.DependencyInjection.Generation.MethodBodyGenerating.Services.InstantiatingGenerators;
using Photosphere.DependencyInjection.Generation.MethodBodyGenerating.ValueObjects;
using Photosphere.DependencyInjection.Lifetimes.Scopes.Services;

namespace Photosphere.DependencyInjection.Generation.MethodBodyGenerating.Services.GeneratingStrategies
{
    internal class PerContainerProvidingGeneratingStrategy : GeneratingStrategyBase, IPerContainerProvidingGeneratingStrategy
    {
        private readonly IScopeKeeper _scopeKeeper;
        private readonly IObjectInstantiatingGenerator _objectInstantiatingGenerator;

        public PerContainerProvidingGeneratingStrategy(
            IScopeKeeper scopeKeeper,
            IObjectInstantiatingGenerator objectInstantiatingGenerator)
        {
            _scopeKeeper = scopeKeeper;
            _objectInstantiatingGenerator = objectInstantiatingGenerator;
        }

        protected override void GenerateDependencyProviding(GeneratingDesign design)
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
                    .Action(() => _objectInstantiatingGenerator.Generate(design))
                    .PopFromStackTo(instanceVariable)
                    .LoadArgumentToStack(0)
                    .SetArrayRefElement(instanceIndex, instanceVariable)
                .EndBranch()
                .PushToStack(instanceVariable)
                .CastToClass(design.ObjectGraph.ImplementationType);
        }
    }
}