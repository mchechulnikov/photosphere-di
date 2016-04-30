# Photosphere.DependencyInjection
## About
Simplest dependency injection framework based on System.Reflection.Emit.

## Status
[![Windows build Status](https://ci.appveyor.com/api/projects/status/github/sunloving/photosphere-di?retina=true&svg=true)](https://ci.appveyor.com/project/sunloving/photosphere-di)

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
var container = new DependencyContainer();
container.Initialize();
var foo = container.GetInstance<IFoo>();
foo.DoSomething();
```

## License
[![license](https://img.shields.io/github/license/mashape/apistatus.svg?maxAge=2592000)]()

## Other Photosphere projects
* [Photosphere.Mapping](https://github.com/sunloving/photosphere-mapping)
