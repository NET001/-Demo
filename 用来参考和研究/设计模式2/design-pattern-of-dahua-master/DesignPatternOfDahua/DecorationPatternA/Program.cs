using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecorationPatternA
{
    /// <summary>
    /// 装饰模式
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            ConcreteComponent concreteComponent = new ConcreteComponent();
            ConcreteDecoratorA decoratorA = new ConcreteDecoratorA();
            ConcreteDecoratorB decoratorB = new ConcreteDecoratorB();
            decoratorA.SetComponent(concreteComponent);
            decoratorB.SetComponent(decoratorA);
            decoratorB.Show();
            Console.ReadKey();
        }
    }
}
