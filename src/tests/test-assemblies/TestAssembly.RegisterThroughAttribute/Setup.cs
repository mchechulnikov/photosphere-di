using Photosphere.DependencyInjection;
using Photosphere.DependencyInjection.Attributes;
using TestAssembly.RegisterThroughAttribute;

[assembly: RegisterDependencies(typeof(Foo))]
[assembly: RegisterDependencies(typeof(Bar), Lifetime.AlwaysNew)]

[assembly: RegisterDependenciesBy(typeof(TestRegisterAttribute))]