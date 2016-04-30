using System;

namespace Photosphere.DependencyInjection.Generators.ObjectGraphs.Exceptions
{
    internal class DetectCircleDependencyException : Exception
    {
        private readonly Type _type;

        public DetectCircleDependencyException(Type type)
        {
            _type = type;
        }

        public override string Message => $"Founded circle dependency in: `{_type}`";
    }
}