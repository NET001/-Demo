using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleFactory_CJL
{
    public class ConcreteProductDefault : IProduct
    {
        public ConcreteProductDefault()
        {
            Name = "默认产品";
        }
        public string Name { get; set; }

        public void Operation()
        {
            Console.WriteLine("产品名称:" + Name);
            Console.WriteLine("不执行操作");
        }
    }
}
