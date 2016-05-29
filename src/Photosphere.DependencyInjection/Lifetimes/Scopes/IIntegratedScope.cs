using System;
using System.Reflection.Emit;

namespace Photosphere.DependencyInjection.Lifetimes.Scopes
{
    internal interface IIntegratedScope : IScope
    {
        void Add(Type type, LocalBuilder instanceVariable);
    }
}