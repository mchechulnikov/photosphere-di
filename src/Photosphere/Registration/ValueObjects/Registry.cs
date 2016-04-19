using System;
using System.Collections.Concurrent;

namespace Photosphere.Registration.ValueObjects
{
    internal class Registry : ConcurrentDictionary<Type, Delegate>, IRegistry {}
}