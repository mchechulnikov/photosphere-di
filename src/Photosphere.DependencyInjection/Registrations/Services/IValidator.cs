namespace Photosphere.DependencyInjection.Registrations.Services
{
    internal interface IValidator
    {
        void Validate<TService, TImplementation>();
    }
}