using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleFactory_CJL
{
    /// <summary>
    /// 默认
    /// </summary>
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
