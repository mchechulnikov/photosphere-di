using System;
using System.Collections.Generic;
using System.Reflection;
using Photosphere.DependencyInjection.Generators.MethodBodyGenerating.Services.Strategies;

namespace Photosphere.DependencyInjection.Generators.ObjectGraphs.DataTransferObjects
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