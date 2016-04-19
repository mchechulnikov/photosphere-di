using System;

namespace Photosphere.CilEmitting.Services
{
    internal interface IInstantiateMethodGenerator
    {
        Func<TTarget> Generate<TTarget>();
    }
}