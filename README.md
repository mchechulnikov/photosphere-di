# Photosphere.DependencyInjection
Simplest dependency injection framework based on emitting CIL code.

## Status
[![Windows build Status](https://ci.appveyor.com/api/projects/status/github/sunloving/photosphere-di?retina=true&svg=true)](https://ci.appveyor.com/project/sunloving/photosphere-di)
[![NuGet](https://img.shields.io/nuget/v/Photosphere.DependencyInjection.svg)](https://www.nuget.org/packages/Photosphere.DependencyInjection/)

## Install via NuGet
```
PM> Install-Package Photosphere.DependencyInjection
```

## Features
### Fast resolving
This container based on building dynamic methods (using `System.Reflection.Emit`) for every registered service. It is very speeds up the resolving of dependencies when requested.

### Preferred declarative style
Container must be configured in a composition root of project/assembly.
``` C#
internal class CompositionRoot : ICompositionRoot
{
  public void Compose(IRegistrator registrator)
  {
    registrator
      .Register<IFoo>()
      .Register<IBar>();
  }
}
```

### Facilitate low coupling
Service can be registered just by interface: search of implementation is carried out in the registration process. It reduce horrible registration mappings that bring only redundant references through a code.<br/>
Just use `Register<TService>` method:
``` C#
registrator.Register<IFoo>();
registrator.Register<IBar>();
```
for register implementations of `IFoo` and `IBar`.

### Registering all descendants by common interface
``` C#
interface IService {}
interface IFoo : IService{}
class Foo : IFoo {}
interface IBar : IService {}
class Bar : IBar
{
  public Bar(IFoo foo) {}
}
```
``` C#
registrator.Register<IService>();
```
``` C#
var foo = registrator.GetInstance<IFoo>();
var bar = registrator.GetInstance<Bar>();
```

### Object graph analysis performed at registration time
Detects and denies cycles and not registered dependencies while service registration.

#### Cycle example
``` C#
class Foo { public Foo(Bar bar) {} }
class Bar { public Bar(Buz buz) {} }
class Buz { public Buz(Foo foo) {} }
```

### Control over objects life
Provides three strategies of managing of lifetime: services can be always created anew, lives only during the time of the request or has container bounded life. Not uber feature :)

### Easy to use
Register:
``` C#
internal class CompositionRoot : ICompositionRoot
{
  public void Compose(IRegistrator registrator)
  {
    registrator
      .Register<IFoo>()
      .Register<IBar>(Lifetime.AlwaysNew)
      .Register<IBuz>(Lifetime.PerRequest)
      .Register<IQiz>(Lifetime.PerContainer);
  }
}
```
...and resolve:
``` C#
var container = new DependencyContainer();
var foo = container.GetInstance<IFoo>();
foo.DoSomething();
```

### Collections injecting
``` C#
var foos = container.GetInstance<IEnumerable<IFoo>>();
```
or
``` C#
var foos = container.GetAllInstances<IFoo>();
```
or
``` C#
class Bar
{
  public Bar(IEnumerable<IFoo> foos) {}
}
```
Instead `IEnumerable` can be used `IReadOnlyCollection` that can be preffered for more clean OOP style.

### Disposable
``` C#
using (var container = new DependencyContainer())
{
  // Objects that resolved here will beсome unreacheble outside this scope 
}
```

## License
[![license](https://img.shields.io/github/license/mashape/apistatus.svg?maxAge=2592000)]()

## Other Photosphere projects
* [Photosphere.Mapping](https://github.com/sunloving/photosphere-mapping)
* [Photosphere.MemoryAllocation](https://github.com/sunloving/photosphere-memalloc)
