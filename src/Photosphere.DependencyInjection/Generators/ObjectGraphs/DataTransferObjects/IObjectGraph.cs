using System;
using System.Collections.Generic;
using System.Reflection;

namespace Photosphere.DependencyInjection.Generators.ObjectGraphs.DataTransferObjects
{
    internal interface IObjectGraph
    {
        Type Type { get; }

        object RegisteredInstance { get; }

        ConstructorInfo Constructor { get; }

        IReadOnlyList<ObjectGraph> Children { get; }
    }
}