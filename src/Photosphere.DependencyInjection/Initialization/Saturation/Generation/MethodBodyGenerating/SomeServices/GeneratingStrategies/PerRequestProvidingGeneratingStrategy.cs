using System.Reflection.Emit;
using Photosphere.DependencyInjection.Initialization.Saturation.Generation.MethodBodyGenerating.SomeServices.InstantiatingGenerators;
using Photosphere.DependencyInjection.Initialization.Saturation.Generation.MethodBodyGenerating.ValueObjects;
using Photosphere.DependencyInjection.LifetimeManagement;

namespace Photosphere.DependencyInjection.Initialization.Saturation.Generation.MethodBodyGenerating.SomeServices.GeneratingStrategies
{
    internal class PerRequestProvidingGeneratingStrategy : GeneratingStrategyBase, IPerRequestProvidingGeneratingStrategy
    {
        private readonly IScopeKeeper _scopeKeeper;
        private readonly IObjectInstantiatingGenerator _objectInstantiatingGenerator;

        public PerRequestProvidingGeneratingStrategy(
            IScopeKeeper scopeKeeper,
            IObjectInstantiatingGenerator objectInstantiatingGenerator)
        {
            _scopeKeeper = scopeKeeper;
            _objectInstantiatingGenerator = objectInstantiatingGenerator;
        }

        protected override void GenerateDependencyProviding(GeneratingDesign design)
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
                        _objectInstantiatingGenerator.Generate(design);
                    })
                    .Variable;
            }
            design.Designer.PushToStack(instanceVariable);
        }
    }
}