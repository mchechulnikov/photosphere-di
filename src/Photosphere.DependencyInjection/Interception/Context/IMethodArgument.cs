using System;

namespace Photosphere.DependencyInjection.Interception.Context
{
    public interface IMethodArgument
    {
        Type Type { get; }
        string Name { get; }
        object Value { get; set; }
    }
}