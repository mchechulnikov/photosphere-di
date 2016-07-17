using Photosphere.DependencyInjection;
using Photosphere.DependencyInjection.Attributes;
using Photosphere.DependencyInjection.TestAssembly.Stress;

[assembly: RegisterDependencies(typeof(IStressService), Lifetime.AlwaysNew)]