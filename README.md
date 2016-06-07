# Photosphere.DependencyInjection
Simplest dependency injection framework based on emitting CIL code.

## Status
[![Windows build Status](https://ci.appveyor.com/api/projects/status/github/sunloving/photosphere-di?retina=true&svg=true)](https://ci.appveyor.com/project/sunloving/photosphere-di)

## Features
* Building dynamic methods (using `System.Reflection.Emit`) for every registered service. It is very speeds up the resolving of dependencies when requested.
* Detects and denies cycles and not registered dependencies while service registration.
* Service can be registered just by interface: search of implementation is carried out in the registration process. It reduce horrible registration mappings that bring only redundant references throught the code.
* Provides three strategies of managing of lifetime: services can be always created anew, lives only during the time of the request or has container bounded life.
* Container must be configured in a composition root of project/assembly.

## Example
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
And resolve:
``` C#
var container = new DependencyContainer();
var foo = container.GetInstance<IFoo>();
foo.DoSomething();
```

## License
[![license](https://img.shields.io/github/license/mashape/apistatus.svg?maxAge=2592000)]()

## Other Photosphere projects
* [Photosphere.Mapping](https://github.com/sunloving/photosphere-mapping)
