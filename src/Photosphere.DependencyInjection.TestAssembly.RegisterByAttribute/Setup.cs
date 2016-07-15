using Photosphere.DependencyInjection.Attributes;
using Photosphere.DependencyInjection.Lifetimes;
using Photosphere.DependencyInjection.TestAssembly.RegisterByAttribute;

[assembly: RegisterDependencies(typeof(Foo))]
[assembly: RegisterDependencies(typeof(Bar), Lifetime.AlwaysNew)]

[assembly: RegisterDependenciesBy(typeof(TestRegisterAttribute))]