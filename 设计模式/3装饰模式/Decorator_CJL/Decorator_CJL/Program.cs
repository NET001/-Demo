using System;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;

namespace Decorator_CJL
{
    class Program
    {
        static void Main(string[] args)
        {
            Demo2();
            Console.ReadLine();
        }
        //装饰类作为中间层
        static void Demo1()
        {
            ConcreteComponent c = new ConcreteComponent();
            ConcreteDecoratorA d1 = new ConcreteDecoratorA();
            ConcreteDecoratorB d2 = new ConcreteDecoratorB();
            //往对象装饰A行为
            d1.SetComponent(c);
            //继续装饰B行为
            d2.SetComponent(d1);
            //执行
            d2.Operation();
        }
        //所有类都继承装饰类
        static void Demo2()
        {
            ConcreteComponent2 c1 = new ConcreteComponent2();
            ConcreteDecorator2A d1 = new ConcreteDecorator2A(c1);
            ConcreteDecorator2B d2 = new ConcreteDecorator2B(d1);
            foreach (var item in d2.Operation())
            {
                Console.WriteLine(item);
            }
        }

    }
}
