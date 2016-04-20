# Photosphere.DependencyInjection
## About
Simplest dependency injection framework based on System.Reflection.Emit.

## Status
[![Windows build Status](https://ci.appveyor.com/api/projects/status/github/sunloving/photosphere-di?retina=true&svg=true)](https://ci.appveyor.com/project/sunloving/photosphere-di)</br>
:warning: Not working yet.

## Example
Register:
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
And resolve:
``` C#
var resolver = new DependencyResolver();
resolver.Initialize();
var foo = resolver.GetInstance<IFoo>();
foo.DoSomething();
```
## License
[![license](https://img.shields.io/github/license/mashape/apistatus.svg?maxAge=2592000)]()
