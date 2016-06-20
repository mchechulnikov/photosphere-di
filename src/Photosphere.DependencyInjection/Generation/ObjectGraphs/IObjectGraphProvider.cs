using System;
using System.Collections.Generic;
using Photosphere.DependencyInjection.Generation.ObjectGraphs.DataTransferObjects;

namespace Photosphere.DependencyInjection.Generation.ObjectGraphs
{
    internal interface IObjectGraphProvider
    {
        IObjectGraph Provide(Type serviceType, ISet<Type> alreadyProvidedTypes = null);
    }
}