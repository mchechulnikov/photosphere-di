using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit.Sdk;

namespace Photosphere.DependencyInjection.xUnit
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class InjectDependencyAttribute : DataAttribute
    {
        private readonly Stack<object> _inlineData;

        public InjectDependencyAttribute()
        {
            _inlineData = new Stack<object>();
        }

        public InjectDependencyAttribute(params object[] data)
        {
            _inlineData = new Stack<object>(data.Reverse());
        }

        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            var container = new DependencyContainer();
            var parameters = testMethod.GetParameters();
            return new[] { parameters.Select(p => GetInstance(p, container)).ToArray() };
        }

        private object GetInstance(ParameterInfo parameter, IDependencyContainer container)
        {
            return _inlineData.Count > 0
                ? _inlineData.Pop()
                : container.GetInstance(parameter.ParameterType);
        }
    }
}
