using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleFactory_CJL
{
    /// <summary>
    /// B号产品
    /// </summary>
    public class ConcreteProductB : IProduct
    {
        public ConcreteProductB(string name)
        {
            Name = name;
            Console.WriteLine("ConcreteProductB初始化了");
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
