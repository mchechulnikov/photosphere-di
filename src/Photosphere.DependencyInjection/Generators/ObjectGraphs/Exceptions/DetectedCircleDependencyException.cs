using System;

namespace Photosphere.DependencyInjection.Generators.ObjectGraphs.Exceptions
{
    internal class DetectedCircleDependencyException : Exception
    {
        private readonly Type _type;

        public DetectedCircleDependencyException(Type type)
        {
            _type = type;
        }

        public override string Message => $"Founded circle dependency in `{_type}`";
    }
}