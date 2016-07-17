using System;
using System.Collections.Generic;

namespace Photosphere.DependencyInjection.Interception.Context
{
    public interface IMethodInvocationContext
    {
        Type MethodType { get; }
        string MethodName { get; }
        Type MethodReturnType { get; }
        object MethodReturnValue { get; set; }
        IReadOnlyCollection<IMethodArgument> Arguments { get; }
    }
}