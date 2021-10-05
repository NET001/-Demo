using System;

namespace Decorator_CJL
{
    class Program
    {
        static void Main(string[] args)
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
            Console.Read();
        }
    }
}
