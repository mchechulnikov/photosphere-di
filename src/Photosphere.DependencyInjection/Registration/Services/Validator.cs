using Photosphere.Registration.Exceptions;

namespace Photosphere.Registration.Services
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