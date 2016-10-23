using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Photosphere.DependencyInjection;
using Photosphere.DependencyInjection.Attributes;
using TestAssembly.Enumerable.TestObjects;

[assembly: AssemblyTitle("TestAssembly.Enumerable2")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("TestAssembly.Enumerable2")]
[assembly: AssemblyCopyright("Copyright ©  2016")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]
[assembly: Guid("22a5a087-f628-4938-9647-0aa5c74bdf0d")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]

[assembly: InternalsVisibleTo("Photosphere.Di.IntegrationTests")]

[assembly: RegisterDependencies(typeof(IBuzzy), Lifetime.PerContainer)]