# Photosphere.DependencyInjection
Simple .NET dependency injection framework based on emitting IL code at service registration time.

## Status
[![Windows build Status](https://ci.appveyor.com/api/projects/status/github/sunloving/photosphere-di?retina=true&svg=true)](https://ci.appveyor.com/project/sunloving/photosphere-di)
[![NuGet](https://img.shields.io/nuget/v/Photosphere.DependencyInjection.svg)](https://www.nuget.org/packages/Photosphere.DependencyInjection/)
[![license](https://img.shields.io/github/license/mashape/apistatus.svg?maxAge=2592000)](https://github.com/sunloving/photosphere-di/blob/master/LICENSE)

## Install via NuGet
```
PM> Install-Package Photosphere.DependencyInjection
```

## Features
* [Fast resolving](https://github.com/sunloving/photosphere-di#fast-resolving)
* [Preferred declarative style](https://github.com/sunloving/photosphere-di#preferred-declarative-style)
* [Facilitate low coupling](https://github.com/sunloving/photosphere-di#facilitate-low-coupling)
* [Registering all descendants by common interface](https://github.com/sunloving/photosphere-di#registering-all-descendants-by-common-interface)
* [Type registration by attribute on base type](https://github.com/sunloving/photosphere-di#type-registration-by-attribute-on-base-type)
* [Object graph analysis performed at registration time](https://github.com/sunloving/photosphere-di#object-graph-analysis-performed-at-registration-time)
* [Control over objects life](https://github.com/sunloving/photosphere-di#control-over-objects-life)
* [Easy to use](https://github.com/sunloving/photosphere-di#easy-to-use)
* [Collections injecting](https://github.com/sunloving/photosphere-di#collections-injecting)
* [Generic registration](https://github.com/sunloving/photosphere-di#generic-registration)
* [Disposable](https://github.com/sunloving/photosphere-di#disposable)
* [Integrations](https://github.com/sunloving/photosphere-di#integrations)

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
You can hint composition root type for more faster container initialization
``` C#
[assembly: CompositionRoot(typeof(FooCompositionRoot))]
```
Light container setup directly into attributes (instead `CompositionRoot`)
``` C#
[assembly: RegisterDependencies(typeof(IService))]
[assembly: RegisterDependencies(typeof(IFoo), Lifetime.AlwaysNew)]
```
These methods of registration are resolved as follows:
* at the first container try to search `CompositionRootAttribute` and use it;
* if it wasn't founded, container try to get up registration info from `RegisterDependenciesAttribute` and `RegisterDependenciesByAttribute`;
* if these attributes wasn't founded the whole-types-search for `ICompositionRoot` implementations will be performed.

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
### Type registration by attribute on base type
If you don't want to register by interface or base type you can use your own custom attribute for it.
``` C#
class ServiceAttribute : Attribute {}
```
``` C#
[Service]
class Foo {}

class Bar : Foo {}
```
Registration
``` C#
registrator.RegisterBy<ServiceAttribute>();
```
or
``` C#
[assembly: RegisterDependenciesBy(typeof(ServiceAttribute))]
```
Providing
``` C#
var foo = container.GetInstance<Foo>();
var bar = container.GetInstance<Bar>();
```
This attribute can be applyed to interfaces too.

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

### Generic registration
You can register generic service
``` C#
registrator.Register(typeof(IGenericService<>))
```
and receive constructed type
``` C#
var foo = container.GetInstance<IGenericService<IFoo>>();
```
or receive multiple constructed type
``` C#
var bars = container.GetAllInstances<IGenericService<Bar>>();
```

### Disposable
``` C#
using (var container = new DependencyContainer())
{
  // Objects that resolved here will beсome unreacheble outside this scope
}
```

### Integrations
* [Photosphere.DependencyInjection.xUnit](https://github.com/sunloving/photosphere-di-xunit)
