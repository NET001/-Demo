using System;
using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Linq;

namespace DI_CJL
{
    /*
        重要对象
        IServiceCollection
        IServiceProvider
        
     
     */
    class Program
    {
        static void Main(string[] args)
        {
            //ServiceCollection为IServiceCollection的默认实现,最后的BuildServiceProvider方法构建了一个容器对象


            IServiceCollection serviceCollection = new ServiceCollection()
              .AddTransient<IFoo, Foo>()
              .AddScoped<IBar, Bar>();
            //Tryxxx扩展,若存在就不会在添加
            serviceCollection.TryAddTransient<IFoo, Foo>();

            //因为实现了IList实际上是一个集合装的是ServiceDescriptor对象
            serviceCollection.Add(ServiceDescriptor.Singleton<IBaz, Baz>());
            ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();




            //从容器中获取对象实例
            IBaz baz = serviceProvider.GetService<IBaz>();
            //原型模式
            IServiceProvider serviceProvider2 = GetNewServiceProvider(serviceProvider);
            //获取服务对象(若不存在返回null)
            IFoo foo = serviceProvider2.GetService<IFoo>();
            //获取服务对象(若不存在抛出异常)
            IBar bar = serviceProvider2.GetRequiredService<IBar>();



            Console.ReadKey();
        }

        /// <summary>
        /// 获得一个新（子）容器对象
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        private static IServiceProvider GetNewServiceProvider(IServiceProvider serviceProvider)
        {
            return serviceProvider.CreateScope().ServiceProvider;
        }

        /// <summary>
        /// 注入泛型
        /// </summary>
        private static IServiceProvider InjectGenericity()
        {
            return new ServiceCollection().AddTransient(typeof(IFoobar<,>), typeof(Foobar<,>)).BuildServiceProvider();
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

    public class IFoobar<T1, T2> { }
    public class Foobar<T1, T2> : IFoobar<T1, T2> { }


}
