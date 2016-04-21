using System;

namespace Photosphere.DependencyInjection.Registration.Exceptions
{
    internal class NotImplementsException<TService, TImplementation> : Exception
    {
        private static readonly string ImplementationTypeName = typeof(TImplementation).FullName;
        private static readonly string ServiceTypeName = typeof(TService).FullName;

        public override string Message => $"Type `{ImplementationTypeName}` not implements `{ServiceTypeName}`";
    }
}