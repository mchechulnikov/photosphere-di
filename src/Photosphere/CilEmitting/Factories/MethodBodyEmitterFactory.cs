using System.Reflection.Emit;
using Photosphere.CilEmitting.Emitters;

namespace Photosphere.CilEmitting.Factories
{
    internal class MethodBodyEmitterFactory : IMethodBodyEmitterFactory
    {
        public IMethodBodyEmitter Get<TTarget>(ILGenerator generator)
        {
            return new InstantiateMethodBodyEmitter(generator, typeof(TTarget));
        }
    }
}