using Photosphere.DependencyInjection.Interception.Context;
using Photosphere.DependencyInjection.Interception.Context.DataTransferObjects;

namespace Photosphere.DependencyInjection.Interception
{
    public interface IMethodInterceptor
    {
        void Intercept(IMethodInvocationContext context);
    }
}