using System;
using System.Collections.Generic;
using System.Reflection;
using Photosphere.DependencyInjection.Initialization.Saturation.Generation.MethodBodyGenerating.Services.GeneratingStrategies;

namespace Photosphere.DependencyInjection.Initialization.Saturation.Generation.ObjectGraphs.DataTransferObjects
{
    internal class ObjectGraph : IObjectGraph
    {
        public Type ReturnType { get; set; }

        public Type ImplementationType { get; set; }

        public ConstructorInfo Constructor { get; set; }

        public IReadOnlyList<IObjectGraph> Children { get; set; }

        public IGeneratingStrategy GeneratingStrategy { get; set; }
    }
}