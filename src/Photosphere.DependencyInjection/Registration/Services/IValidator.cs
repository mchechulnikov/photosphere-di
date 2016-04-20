namespace Photosphere.Registration.Services
{
    internal interface IValidator
    {
        void Validate<TService, TImplementation>();
    }
}