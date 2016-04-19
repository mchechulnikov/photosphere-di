using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Photosphere.Registration.ValueObjects
{
    internal interface IRegistry : IDictionary<Type, Delegate> {}

    internal class Registry : ConcurrentDictionary<Type, Delegate>, IRegistry {}
}