using System;
using System.Collections.Generic;

namespace SimpleFactory_CJL
{
    class Program
    {
        static void Main(string[] args)
        {
            SimpleFactory simpleFactory = new SimpleFactory();
            //使用工厂初始化了3个产品对象
            List<IProduct> products = new List<IProduct>() {
                simpleFactory.GetProduct("产品A"),
                simpleFactory.GetProduct("产品B"),
                simpleFactory.GetProduct(),
            };
            //执行方法,对于初始化的工作主要交给了工厂类,Main方法只通过了简单的逻辑判断就获得了对象的实例
            foreach (var product in products)
            {
                product.Operation();
            }
            Console.ReadKey();
        }
    }
}
