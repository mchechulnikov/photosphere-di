using System;

namespace Photosphere.DependencyInjection.Interception.Context.DataTransferObjects
{
    internal class MethodArgument : IMethodArgument
    {
        public Type Type { get; }

        public string Name { get; }

        public object Value { get; set; }
    }
}