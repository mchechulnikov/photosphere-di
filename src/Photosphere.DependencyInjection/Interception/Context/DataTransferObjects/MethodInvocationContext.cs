using System;
using System.Collections.Generic;

namespace Photosphere.DependencyInjection.Interception.Context.DataTransferObjects
{
    internal class MethodInvocationContext : IMethodInvocationContext
    {
        public Type MethodType { get; }

        public string MethodName { get; }

        public Type MethodReturnType { get; }

        public object MethodReturnValue { get; set; }

        public IReadOnlyCollection<IMethodArgument> Arguments { get; }
    }
}