using System.Reflection.Emit;

namespace Photosphere.CilEmitting.Emitters
{
    internal interface IMethodBodyEmitter
    {
        LocalBuilder Emit();
    }
}