using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleFactory_CJL
{
    public class ConcreteProductA : IProduct
    {
        public ConcreteProductA()
        {
            Name = "产品A";
        }
        public string Name { get; set; }

        public void Operation()
        {
            Console.WriteLine("产品名称:" + Name);
            Console.WriteLine("执行操作1:" + Name);
            Console.WriteLine("执行操作2:" + Name);
        }
    }
}
