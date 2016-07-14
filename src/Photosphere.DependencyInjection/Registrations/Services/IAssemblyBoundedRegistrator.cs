using System;
using System.Reflection;
using Photosphere.DependencyInjection.Lifetimes;

namespace Photosphere.DependencyInjection.Registrations.Services
{
    internal interface IAssemblyBoundedRegistrator
    {
        void Register(Type serviceType, Assembly assembly, Lifetime lifetime);

        void RegisterBy(Type attributeType, Assembly assembly, Lifetime lifetime);
    }
}