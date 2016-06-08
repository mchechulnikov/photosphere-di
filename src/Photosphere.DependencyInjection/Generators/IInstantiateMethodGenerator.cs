using System;

namespace Photosphere.DependencyInjection.Generators
{
    internal interface IInstantiateMethodGenerator
    {
        Delegate Generate(Type serviceType);
    }
}