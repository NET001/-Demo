using System;
using System.Collections.Generic;
using System.Text;

namespace FactoryMethod_CJL
{
    public class ConcreteProductA : Product
    {
        public void Operation()
        {
            Console.WriteLine("执行a操作");
        }
    }
}
