using System;
using System.Reflection;

namespace Photosphere.DependencyInjection.Initialization.Registrations
{
    internal interface IAssemblyBoundedRegistrator
    {
        void Register(Type serviceType, Assembly assembly, Lifetime lifetime);

        void RegisterBy(Type attributeType, Assembly assembly, Lifetime lifetime);
    }
}