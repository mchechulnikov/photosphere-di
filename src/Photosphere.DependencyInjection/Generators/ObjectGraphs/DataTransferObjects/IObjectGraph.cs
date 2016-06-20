using System;
using System.Collections.Generic;
using System.Reflection;
using Photosphere.DependencyInjection.Generators.MethodBodyGenerating.Strategies;

namespace Photosphere.DependencyInjection.Generators.ObjectGraphs.DataTransferObjects
{
    internal interface IObjectGraph
    {
        Type ReturnType { get; }

        Type ImplementationType { get; }

        ConstructorInfo Constructor { get; }

        IReadOnlyList<IObjectGraph> Children { get; }

        IGeneratingStrategy GeneratingStrategy { get; }
    }
}