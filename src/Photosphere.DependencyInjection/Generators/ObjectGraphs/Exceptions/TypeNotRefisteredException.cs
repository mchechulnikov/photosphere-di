using System;

namespace Photosphere.DependencyInjection.Generators.ObjectGraphs.Exceptions
{
    internal class TypeNotRefisteredException : Exception
    {
        private readonly Type _type;

        public TypeNotRefisteredException(Type type)
        {
            _type = type;
        }

        public override string Message => $"Type `{_type}` not registered";
    }
}