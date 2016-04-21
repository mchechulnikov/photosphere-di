using Photosphere.DependencyInjection.Registration.Exceptions;

namespace Photosphere.DependencyInjection.Registration.Services
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