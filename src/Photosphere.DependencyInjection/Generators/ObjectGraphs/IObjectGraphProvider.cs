using System;
using System.Collections.Generic;
using Photosphere.DependencyInjection.Generators.ObjectGraphs.DataTransferObjects;

namespace Photosphere.DependencyInjection.Generators.ObjectGraphs
{
    internal interface IObjectGraphProvider
    {
        IObjectGraph Provide(Type serviceType, ISet<Type> alreadyProvidedTypes = null);
    }
}