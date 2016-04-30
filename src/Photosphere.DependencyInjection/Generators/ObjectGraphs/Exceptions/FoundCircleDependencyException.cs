using System;

namespace Photosphere.DependencyInjection.Generators.ObjectGraphs.Exceptions
{
    internal class FoundCircleDependencyException : Exception
    {
        public override string Message => $"Founded circle dependency";
    }
}