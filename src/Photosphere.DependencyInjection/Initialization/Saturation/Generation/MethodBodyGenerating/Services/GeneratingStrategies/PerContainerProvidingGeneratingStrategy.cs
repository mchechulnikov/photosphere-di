using System;
using Photosphere.DependencyInjection.Initialization.Saturation.Generation.MethodBodyGenerating.Services.InstantiatingGenerators;
using Photosphere.DependencyInjection.Initialization.Saturation.Generation.MethodBodyGenerating.ValueObjects;
using Photosphere.DependencyInjection.LifetimeManagement;

namespace Photosphere.DependencyInjection.Initialization.Saturation.Generation.MethodBodyGenerating.Services.GeneratingStrategies
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
            lock (this)
            {
                var instanceIndex = GetInstanceIndex(design.ObjectGraph.ImplementationType);
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

        private int GetInstanceIndex(Type implementationType)
        {
            var scope = _scopeKeeper.PerContainerScope;
            int instanceIndex;
            if (scope.AvailableInstancesIndexes.TryGetValue(implementationType, out instanceIndex))
            {
                return instanceIndex;
            }
            instanceIndex = scope.AvailableInstancesIndexes.Count;
            scope.AvailableInstancesIndexes.AddOrUpdate(implementationType, t => instanceIndex, (t, v) => v);
            return instanceIndex;
        }
    }
}