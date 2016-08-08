using Photosphere.DependencyInjection.Interception.Context;

namespace Photosphere.DependencyInjection.Interception
{
    public interface IMethodInterceptor
    {
        void Intercept(IMethodInvocationContext context);
    }
}