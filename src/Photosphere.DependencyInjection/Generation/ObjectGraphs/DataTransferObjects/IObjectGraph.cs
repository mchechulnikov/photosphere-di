using System;
using System.Collections.Generic;
using System.Reflection;
using Photosphere.DependencyInjection.Generation.MethodBodyGenerating.Services.GeneratingStrategies;

namespace Photosphere.DependencyInjection.Generation.ObjectGraphs.DataTransferObjects
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