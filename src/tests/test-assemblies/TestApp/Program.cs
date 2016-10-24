using Photosphere.DependencyInjection;
using Photosphere.DependencyInjection.Attributes;
using TestApp;

[assembly: RegisterDependencies(typeof(IFoo))]

namespace TestApp
{
    /// <summary>
    /// This app used for profiling DI functionality
    /// </summary>
    internal class Program
    {
        public static void Main(string[] args)
        {
            var container = new DependencyContainer();
            var instances = container.GetInstance<Foo0>();
        }
    }

    public interface IFoo {}
    internal class Foo50 : IFoo {}
    internal class Foo49 : IFoo { public Foo49(Foo50 foo) {} }
    internal class Foo48 : IFoo { public Foo48(Foo49 foo) {} }
    internal class Foo47 : IFoo { public Foo47(Foo48 foo) {} }
    internal class Foo46 : IFoo { public Foo46(Foo47 foo) {} }
    internal class Foo45 : IFoo { public Foo45(Foo46 foo) {} }
    internal class Foo44 : IFoo { public Foo44(Foo45 foo) {} }
    internal class Foo43 : IFoo { public Foo43(Foo44 foo) {} }
    internal class Foo42 : IFoo { public Foo42(Foo43 foo) {} }
    internal class Foo41 : IFoo { public Foo41(Foo42 foo) {} }
    internal class Foo40 : IFoo { public Foo40(Foo41 foo) {} }
    internal class Foo39 : IFoo { public Foo39(Foo40 foo) {} }
    internal class Foo38 : IFoo { public Foo38(Foo39 foo) {} }
    internal class Foo37 : IFoo { public Foo37(Foo38 foo) {} }
    internal class Foo36 : IFoo { public Foo36(Foo37 foo) {} }
    internal class Foo35 : IFoo { public Foo35(Foo36 foo) {} }
    internal class Foo34 : IFoo { public Foo34(Foo35 foo) {} }
    internal class Foo33 : IFoo { public Foo33(Foo34 foo) {} }
    internal class Foo32 : IFoo { public Foo32(Foo33 foo) {} }
    internal class Foo31 : IFoo { public Foo31(Foo32 foo) {} }
    internal class Foo30 : IFoo { public Foo30(Foo31 foo) {} }
    internal class Foo29 : IFoo { public Foo29(Foo30 foo) {} }
    internal class Foo28 : IFoo { public Foo28(Foo29 foo) {} }
    internal class Foo27 : IFoo { public Foo27(Foo28 foo) {} }
    internal class Foo26 : IFoo { public Foo26(Foo27 foo) {} }
    internal class Foo25 : IFoo { public Foo25(Foo26 foo) {} }
    internal class Foo24 : IFoo { public Foo24(Foo25 foo) {} }
    internal class Foo23 : IFoo { public Foo23(Foo24 foo) {} }
    internal class Foo22 : IFoo { public Foo22(Foo23 foo) {} }
    internal class Foo21 : IFoo { public Foo21(Foo22 foo) {} }
    internal class Foo20 : IFoo { public Foo20(Foo21 foo) {} }
    internal class Foo19 : IFoo { public Foo19(Foo20 foo) {} }
    internal class Foo18 : IFoo { public Foo18(Foo19 foo) {} }
    internal class Foo17 : IFoo { public Foo17(Foo18 foo) {} }
    internal class Foo16 : IFoo { public Foo16(Foo17 foo) {} }
    internal class Foo15 : IFoo { public Foo15(Foo16 foo) {} }
    internal class Foo14 : IFoo { public Foo14(Foo15 foo) {} }
    internal class Foo13 : IFoo { public Foo13(Foo14 foo) {} }
    internal class Foo12 : IFoo { public Foo12(Foo13 foo) {} }
    internal class Foo11 : IFoo { public Foo11(Foo12 foo) {} }
    internal class Foo10 : IFoo { public Foo10(Foo11 foo) {} }
    internal class Foo9 : IFoo { public Foo9(Foo10 foo) {} }
    internal class Foo8 : IFoo { public Foo8(Foo9 foo) {} }
    internal class Foo7 : IFoo { public Foo7(Foo8 foo) {} }
    internal class Foo6 : IFoo { public Foo6(Foo7 foo) {} }
    internal class Foo5 : IFoo { public Foo5(Foo6 foo) {} }
    internal class Foo4 : IFoo { public Foo4(Foo5 foo) {} }
    internal class Foo3 : IFoo { public Foo3(Foo4 foo) {} }
    internal class Foo2 : IFoo { public Foo2(Foo3 foo) {} }
    internal class Foo1 : IFoo { public Foo1(Foo2 foo) {} }
    internal class Foo0 : IFoo { public Foo0(Foo1 foo) {} }
}
