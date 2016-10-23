using Photosphere.DependencyInjection;
using Photosphere.DependencyInjection.Attributes;
using TestAssembly.Stress;

[assembly: RegisterDependencies(typeof(IStressService), Lifetime.AlwaysNew)]