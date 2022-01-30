

using Microsoft.Extensions.DependencyInjection;

class Program
{
    static void Main()
    {
        Demo3();
        Console.Read();
    }
    /// <summary>
    /// 容器的使用
    /// </summary>
    static void Demo1()
    {    //服务注册
        var provider = new ServiceCollection()
            .AddTransient<IFoo, Foo>()
            //方法委托
            .AddScoped<IBar>(_ => new Bar())
            .AddSingleton<IBaz, Baz>()
            //泛型
            .AddTransient(typeof(IFoobar<,>), typeof(Foobar<,>))
            //获取容器
            .BuildServiceProvider();

        //获取子容器
        var provider1 = provider.CreateScope().ServiceProvider;
        var provider2 = provider.CreateScope().ServiceProvider;

        //获取指定类型的服务,基于父类实现的
        var services = provider.GetServices<Base>();
        Console.WriteLine(services.OfType<Foo>().Any());
        Console.WriteLine(services.OfType<Bar>().Any());
        Console.WriteLine(services.OfType<Baz>().Any());

        //生命周期结束会调用注入对象的Dispose()方法
        provider.GetService<IFoo>();
        provider.GetService<IBar>();
        provider.GetService<IBaz>();
    }
    //多个相同实例的注入
    static void Demo2()
    {
        var provider = new ServiceCollection()
            .AddSingleton(new Obj1() { C1 = "1" })
            .AddSingleton(new Obj1() { C1 = "2" })
            .AddSingleton(new Obj1() { C1 = "3" })
            .BuildServiceProvider();
        var objs = provider.GetServices<Obj1>();
    }
    /// <summary>
    /// 修改容器返回的对象
    /// </summary>
    static void Demo3()
    {
        var provider = new ServiceCollection()
            .AddSingleton(new Obj1() { C1 = "1" })
            .BuildServiceProvider();
        provider.GetService<Obj1>().C1 = "2";
        Obj1 obj1 = provider.GetService<Obj1>();
    }
}
public class Obj1
{
    public string C1 { get; set; }
}

public interface IFoo { }
public interface IBar { }
public interface IBaz { }
public interface IFoobar<T1, T2> { }
//
public class Base : IDisposable
{
    public Base() => Console.WriteLine($"An instance of {GetType().Name} is created.");
    public void Dispose() => Console.WriteLine($"The instance of {GetType().Name} is disposed.");
}

public class Foo : Base, IFoo, IDisposable { }
public class Bar : Base, IBar, IDisposable { }
public class Baz : Base, IBaz, IDisposable { }
public class Foobar<T1, T2> : IFoobar<T1, T2>
{
    public IFoo Foo { get; }
    public IBar Bar { get; }
    public Foobar(IFoo foo, IBar bar)
    {
        Foo = foo;
        Bar = bar;
    }
}