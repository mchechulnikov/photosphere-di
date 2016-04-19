using System.Reflection.Emit;

namespace Photosphere.CilEmitting.Services
{
    internal interface IInstantiateMethodBodyEmitter
    {
        LocalBuilder Emit();
    }
}