using System;
using System.Collections.Generic;
using System.Reflection;

namespace Photosphere.DependencyInjection.Generators.ObjectGraphs.DataTransferObjects
{
    internal interface IObjectGraph
    {
        Type ImplementationType { get; }

        object RegisteredInstance { get; }

        ConstructorInfo Constructor { get; }

        IReadOnlyList<IObjectGraph> Children { get; }
    }
}