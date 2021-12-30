using System;
using System.Collections.Generic;
using System.Text;

namespace DI_CJL
{
    public interface IService
    {
        void SayHello();
    }
    public class ServiceA : IService
    {
        public void SayHello()
        {
            Console.WriteLine("Hello,I'm from A");
        }
    }
    public class ServiceB : IService
    {
        private readonly ServiceA _service;

        public ServiceB(ServiceA service)
        {
            _service = service;
        }
        public void SayHello()
        {
            Console.WriteLine("-------------");
            _service.SayHello();
            Console.WriteLine("Hello,I'm from B");
        }
    }

}
