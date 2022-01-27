

using Microsoft.Extensions.DependencyInjection;

class Program
{
    static void Main()
    {
        //服务注册
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