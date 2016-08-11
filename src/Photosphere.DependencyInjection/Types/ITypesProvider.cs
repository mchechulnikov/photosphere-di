using System;
using System.Collections.Generic;
using System.Reflection;

namespace Photosphere.DependencyInjection.Types
{
    internal interface ITypesProvider
    {
        IReadOnlyCollection<Type> GetAllDerivedTypesFrom(Type type, Assembly assembly);
        IReadOnlyCollection<Type> GetMarkedTypes(Type attributeType);
    }
}