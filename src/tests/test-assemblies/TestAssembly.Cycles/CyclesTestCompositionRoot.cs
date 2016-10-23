using Photosphere.DependencyInjection;

namespace TestAssembly.Cycles
{
    public class CyclesTestCompositionRoot : ICompositionRoot
    {
        public void Compose(IRegistrator registrator)
        {
            registrator
                .Register<IFoo>()
                .Register<IBar>()
                .Register<IQiz>();
        }
    }
}