using System;
using System.Collections.Generic;
using Photosphere.DependencyInjection.Initialization.Saturation.Generation.ObjectGraphs.DataTransferObjects;

namespace Photosphere.DependencyInjection.Initialization.Saturation.Generation.ObjectGraphs
{
    internal interface IObjectGraphProvider
    {
        IObjectGraph Provide(Type serviceType, ISet<Type> alreadyProvidedTypes = null);
    }
}