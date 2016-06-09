using System.Collections.Generic;

namespace Photosphere.DependencyInjection.Resolving
{
    internal interface IResolver
    {
        TService GetInstance<TService>();

        IEnumerable<TService> GetAllInstances<TService>();
    }
}