using System;

namespace Photosphere.DependencyInjection.Generation
{
    internal interface IInstanceProvidingMethodGenerator
    {
        Delegate Generate(Type serviceType);
    }
}