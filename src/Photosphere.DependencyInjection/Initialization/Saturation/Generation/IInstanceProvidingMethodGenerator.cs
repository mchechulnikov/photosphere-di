using System;

namespace Photosphere.DependencyInjection.Initialization.Saturation.Generation
{
    internal interface IInstanceProvidingMethodGenerator
    {
        Delegate Generate(Type serviceType);
    }
}