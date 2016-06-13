using Photosphere.DependencyInjection.SystemExtends.Reflection.Emit;

namespace Photosphere.DependencyInjection.Generators.MethodBodyGenerating.Strategies
{
    internal interface IInstantiationGeneratingStrategy
    {
        void Emit();
    }

    internal class AlwaysNewInstantiationGeneratingStrategy : IInstantiationGeneratingStrategy
    {
        private readonly ICilEmitter _ilEmitter;

        public AlwaysNewInstantiationGeneratingStrategy(ICilEmitter ilEmitter)
        {
            _ilEmitter = ilEmitter;
        }

        public void Emit()
        {
            throw new System.NotImplementedException();
        }
    }
}