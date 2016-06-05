using System;

namespace Photosphere.DependencyInjection.Generators
{
    internal interface IInstantiateMethodGenerator
    {
        Func<object[], TTarget> Generate<TTarget>();
    }
}