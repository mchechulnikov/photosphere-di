using System;
using System.Collections.Generic;

namespace Photosphere.DependencyInjection.Resolving
{
    internal interface IResolver
    {
        TService GetInstance<TService>();

        object GetInstance(Type type);

        IEnumerable<TService> GetAllInstances<TService>();
    }
}