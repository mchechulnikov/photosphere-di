using System;
using System.Reflection;

namespace Photosphere.DependencyInjection.Initialization.Registrations
{
    internal interface IInterceptorRegistrator
    {
        void Register(Type interceptorType, Type attributeType, Assembly assembly);
    }
}