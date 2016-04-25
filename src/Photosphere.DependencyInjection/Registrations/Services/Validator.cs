using Photosphere.DependencyInjection.Registrations.Services.Exceptions;

namespace Photosphere.DependencyInjection.Registrations.Services
{
    internal class Validator : IValidator
    {
        public void Validate<TService, TImplementation>()
        {
            var implementationType = typeof(TImplementation);
            if (implementationType.IsInterface || implementationType.IsAbstract)
            {
                throw new NotImplementsException<TService, TImplementation>();
            }
        }
    }
}