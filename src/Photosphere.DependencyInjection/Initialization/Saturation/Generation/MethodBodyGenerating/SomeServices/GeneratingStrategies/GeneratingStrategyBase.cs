using System;
using System.Collections.Concurrent;
using System.Reflection.Emit;
using Photosphere.DependencyInjection.Initialization.Saturation.Generation.MethodBodyGenerating.ValueObjects;

namespace Photosphere.DependencyInjection.Initialization.Saturation.Generation.MethodBodyGenerating.SomeServices.GeneratingStrategies
{
    internal abstract class GeneratingStrategyBase : IGeneratingStrategy
    {
        private readonly ConcurrentDictionary<Type, LocalBuilder> _cache =
            new ConcurrentDictionary<Type, LocalBuilder>();

        public LocalBuilder Generate(GeneratingDesign design)
        {
            //return _cache.GetOrAdd(design.ObjectGraph.ImplementationType, t => GenerateDirectly(design));
            return GenerateDirectly(design);
        }

        protected abstract void GenerateDependencyProviding(GeneratingDesign design);

        private LocalBuilder GenerateDirectly(GeneratingDesign design)
        {
            return design.Designer
                .DeclareVariable(design.ObjectGraph.ReturnType)
                .AssignTo(v => GenerateDependencyProviding(design))
                .Variable;
        }
    }
}