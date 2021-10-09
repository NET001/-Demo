using System;
using System.Collections.Generic;
using System.Text;

namespace FactoryMethod_CJL
{
    public class ConcreteProductB : Product
    {
        public void Operation()
        {
            Console.WriteLine("执行b操作");
        }
    }
}
