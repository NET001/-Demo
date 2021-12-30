using System;

namespace DI_CJL
{
    class Program
    {
        static void Main(string[] args)
        {
            IServiceLocator locator = ServiceLocator.GetInstance();
            locator.AddService<ServiceA>();
            locator.AddService<ServiceB>();
            locator.AddService<IService, ServiceA>();
            locator.AddService<IService, ServiceB>();

            locator.GetService<ServiceA>().SayHello();
            locator.GetService<ServiceB>().SayHello();
            locator.GetService<IService>().SayHello();
            Console.ReadKey();
        }
    }
}
