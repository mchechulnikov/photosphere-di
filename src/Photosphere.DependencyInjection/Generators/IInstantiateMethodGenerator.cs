using System;

namespace Photosphere.DependencyInjection.Generators
{
    internal interface IInstantiateMethodGenerator
    {
        Func<TTarget> Generate<TTarget>();
    }
}