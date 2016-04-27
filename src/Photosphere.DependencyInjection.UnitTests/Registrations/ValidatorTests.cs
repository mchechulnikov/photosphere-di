using Photosphere.DependencyInjection.Registrations.Services;
using Photosphere.DependencyInjection.Registrations.Services.Exceptions;
using Photosphere.DependencyInjection.UnitTests.TestObjects;
using Photosphere.DependencyInjection.UnitTests.TestObjects.Objects;
using Xunit;

namespace Photosphere.DependencyInjection.UnitTests.Registrations
{
    public class ValidatorTests
    {
        [Fact]
        internal void Validate_ValidTypes_ValidResult()
        {
            var validator = new Validator();

            validator.Validate<IFoo, Foo>();
        }

        [Fact]
        internal void Validate_NotImplementedType_Exception()
        {
            var validator = new Validator();

            try
            {
                validator.Validate<IFoo, Bar>();
            }
            catch (NotImplementsException<IFoo, Bar> exception)
            {
                Assert.True(
                   exception.Message.Contains(typeof(IFoo).FullName)
                   && exception.Message.Contains(typeof(Bar).FullName)
               );
            }
        }
    }
}