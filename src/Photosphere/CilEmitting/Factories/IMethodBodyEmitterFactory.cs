using System.Reflection.Emit;
using Photosphere.CilEmitting.Emitters;

namespace Photosphere.CilEmitting.Factories
{
    internal interface IMethodBodyEmitterFactory
    {
        IMethodBodyEmitter Get<TTarget>(ILGenerator generator);
    }
}