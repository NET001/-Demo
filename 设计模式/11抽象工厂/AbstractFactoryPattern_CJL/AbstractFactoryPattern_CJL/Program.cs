using System;

namespace AbstractFactoryPattern_CJL
{
    class Program
    {
        static void Main(string[] args)
        {
            AbstractFactory factory1 = new ConcreteFactory1();
            FactoryProducer c1 = new FactoryProducer(factory1);
            c1.Run();

            AbstractFactory factory2 = new ConcreteFactory2();
            FactoryProducer c2 = new FactoryProducer(factory2);
            c2.Run();

            Console.Read();
        }
    }
}
