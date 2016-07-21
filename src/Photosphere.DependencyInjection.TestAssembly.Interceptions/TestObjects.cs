using System;
using Photosphere.DependencyInjection.Interception;
using Photosphere.DependencyInjection.Interception.Context;

namespace Photosphere.DependencyInjection.TestAssembly.Interceptions
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property)]
    internal class InterceptAttribute : Attribute {}

    internal interface IFoo
    {
        [Intercept]
        int GetNumber(string number);

        [Intercept]
        void DoSomething();
    }

    internal class Foo : IFoo
    {
        public int GetNumber(string number)
        {
            int result;
            int.TryParse(number, out result);
            return result;
        }

        public void DoSomething() {}
    }

    internal class FooInterceptor : IMethodInterceptor
    {
        public void Intercept(IMethodInvocationContext context)
        {
            throw new NotImplementedException();
        }
    }
}