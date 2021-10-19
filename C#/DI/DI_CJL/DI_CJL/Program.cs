using System;

namespace DI_CJL
{
    class Program
    {
        static void Main(string[] args)
        {
            IServiceLocator locator = ServiceLocator.GetInstance();
            locator.AddService<ServiceD>();
            locator.AddService<ServiceA>();

            locator.GetService<ServiceD>().SayHello();
            locator.GetService<ServiceA>().SayHello();
        }
    }
}
