using System;

namespace Photosphere.DependencyInjection.Generators.ObjectGraphs.Exceptions
{
    internal class TypeNotRegisteredException : Exception
    {
        private readonly Type _type;

        public TypeNotRegisteredException(Type type)
        {
            _type = type;
        }

        public override string Message => $"Type `{_type}` not registered";
    }
}