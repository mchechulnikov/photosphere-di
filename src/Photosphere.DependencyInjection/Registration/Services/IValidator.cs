namespace Photosphere.DependencyInjection.Registration.Services
{
    internal interface IValidator
    {
        void Validate<TService, TImplementation>();
    }
}