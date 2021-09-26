using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleFactory_CJL
{
    public class ConcreteProductB : IProduct
    {
        public ConcreteProductB(string name)
        {
            Name = name;
        }
        public string Name { get; set; }
        public void Operation()
        {
            Console.WriteLine("产品名称:" + Name);
            Console.WriteLine("执行操作2:" + Name);
            Console.WriteLine("执行操作3:" + Name);
        }
    }
}
