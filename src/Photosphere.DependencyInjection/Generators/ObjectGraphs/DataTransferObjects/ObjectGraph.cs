using System;
using System.Collections.Generic;
using System.Reflection;

namespace Photosphere.DependencyInjection.Generators.ObjectGraphs.DataTransferObjects
{
    internal class ObjectGraph
    {
        public ObjectGraph(Type type, ConstructorInfo constructor, IReadOnlyList<ObjectGraph> children)
        {
            Type = type;
            Constructor = constructor;
            Children = children;
        }

        public Type Type { get; }

        public ConstructorInfo Constructor { get; }

        public IReadOnlyList<ObjectGraph> Children { get; }
    }
}