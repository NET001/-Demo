using System;
using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace DI_CJL
{
    class Program
    {
        static void Main(string[] args)
        {
            //ServiceCollection为IServiceCollection的默认实现,最后的BuildServiceProvider方法构建了一个容器对象
            ServiceProvider serviceProvider = new ServiceCollection()
                .AddTransient<IFoo, Foo>()
                .AddScoped<IBar, Bar>()
                .AddSingleton<IBaz, Baz>()
                .BuildServiceProvider();
            //从容器中获取对象实例
            IBaz baz = serviceProvider.GetService<IBaz>();


            Console.ReadKey();
        }
    }
    public interface IFoo { }
    public interface IBar { }
    public interface IBaz { }
    public class Foo : IFoo { }
    public class Bar : IBar { }
    public class Baz : IBaz { }
    public class Base : IDisposable
    {
        public Base() => Console.WriteLine($"An instance of{GetType().Name} is created.");

        public void Dispose() => Console.WriteLine($"The instance of{GetType().Name} is dsposed.");
    }


}
