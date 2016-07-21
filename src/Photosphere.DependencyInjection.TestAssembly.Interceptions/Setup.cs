using Photosphere.DependencyInjection.Attributes;
using Photosphere.DependencyInjection.TestAssembly.Interceptions;

[assembly: RegisterDependencies(typeof(IFoo))]
[assembly: RegisterInterceptor(typeof(FooInterceptor), typeof(InterceptAttribute))]