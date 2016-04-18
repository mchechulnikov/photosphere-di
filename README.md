# Photosphere
## About
Simplest dependency injection framework based on System.Reflection.Emit.

## Status
[![Windows build Status](https://ci.appveyor.com/api/projects/status/github/sunloving/photosphere?retina=true&svg=true)](https://ci.appveyor.com/project/sunloving/photosphere)</br>
:warning: Not working yet.

## Example
Register:
``` C#
public interface IFoo {}
public interface IBar {}
public class Foo
{
    private readonly IBar _bar;
    
    public Foo(IBar bar)
    {
        _bar = bar;
    }
    
    public void DoSomething() { /*...*/ }
}

internal class CompositionRoot : ICompositionRoot
{
    public void Compose(IRegistrator registrator)
    {
        registrator
            .Register<IFoo>()
            .Register<IBar, Bar>();
    }
}
```
And resolve:
```
var resolver = new DependencyResolver();
var foo = resolver.GetInstance<IFoo>();
foo.DoSomething();
```
