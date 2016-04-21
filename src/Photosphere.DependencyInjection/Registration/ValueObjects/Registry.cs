using System;
using System.Collections.Concurrent;

namespace Photosphere.DependencyInjection.Registration.ValueObjects
{
    internal class Registry : ConcurrentDictionary<Type, Delegate>, IRegistry {}
}