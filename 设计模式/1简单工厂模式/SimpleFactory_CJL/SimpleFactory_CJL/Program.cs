using System;
using System.Collections.Generic;

namespace SimpleFactory_CJL
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime beforDT = DateTime.Now;
            Demo2();
            DateTime afterDT = DateTime.Now;
            TimeSpan ts = afterDT.Subtract(beforDT);
            Console.WriteLine("DateTime总共花费{0}ms.", ts.TotalMilliseconds);
            Console.ReadKey(); 
        }
        static void Demo1()
        {
            SimpleFactory simpleFactory = new SimpleFactory();
            List<IProduct> products = new List<IProduct>();
            //使用工厂初始化了3个产品对象
            for (int i = 0; i < 1000; i++)
            {
                products = new List<IProduct>() {
                    simpleFactory.GetProduct("产品A"),
                    simpleFactory.GetProduct("产品B"),
                    simpleFactory.GetProduct(),
                };
            }
            //执行方法,对于初始化的工作主要交给了工厂类,Main方法只通过了简单的逻辑判断就获得了对象的实例
            foreach (var product in products)
            {
                product.Operation();
            }
        }
        static void Demo2()
        {
            CacheSimpleFactory simpleFactory = new CacheSimpleFactory();
            List<IProduct> products = new List<IProduct>();
            //使用工厂初始化了3个产品对象
            for (int i = 0; i < 1000; i++)
            {
                products = new List<IProduct>() {
                    simpleFactory.GetProduct("产品A"),
                    simpleFactory.GetProduct("产品B"),
                    simpleFactory.GetProduct(),
                };
            }
            //执行方法,对于初始化的工作主要交给了工厂类,Main方法只通过了简单的逻辑判断就获得了对象的实例
            foreach (var product in products)
            {
                product.Operation();
            }
        }
    }
}
