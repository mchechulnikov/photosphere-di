using System;
using System.Collections.Generic;

namespace Photosphere.DependencyInjection.Registration.ValueObjects
{
    internal interface IRegistry : IDictionary<Type, Delegate> {}
}