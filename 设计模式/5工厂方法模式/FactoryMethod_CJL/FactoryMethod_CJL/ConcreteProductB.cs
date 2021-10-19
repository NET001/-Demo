using System;
using System.Collections.Generic;
using System.Text;

namespace FactoryMethod_CJL
{
    /// <summary>
    /// 具体产品B
    /// </summary>
    public class ConcreteProductB : Product
    {
        public void Operation()
        {
            Console.WriteLine("执行b操作");
        }
    }
}
