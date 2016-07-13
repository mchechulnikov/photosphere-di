using System;

namespace Photosphere.DependencyInjection.IntegrationTests.RegisterByAttribute.TestObjects
{
    [AttributeUsage(AttributeTargets.All, Inherited = false)]
    public class TestRegisterAttribute : Attribute {}
}