using System;
using System.Collections.Generic;

namespace Photosphere.Registration.ValueObjects
{
    internal interface IRegistry : IDictionary<Type, Delegate> {}
}