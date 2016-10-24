using System;
using System.Collections.Generic;
using System.Reflection;
using Photosphere.DependencyInjection.Initialization.Saturation.Generation.MethodBodyGenerating.SomeServices.GeneratingStrategies;

namespace Photosphere.DependencyInjection.Initialization.Saturation.Generation.ObjectGraphs.DataTransferObjects
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