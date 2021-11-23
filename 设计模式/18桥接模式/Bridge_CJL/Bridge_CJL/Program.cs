using System;

namespace Bridge_CJL
{
    class Program
    {
        static void Main(string[] args)
        {
            Abstraction ab1 = new RefinedAbstraction1();
            ab1.SetImplementor(new ConcreteImplementor1B(),new ConcreteImplementor2A());
            ab1.Operation();
            Abstraction ab2 = new RefinedAbstraction2();
            ab2.SetImplementor(new ConcreteImplementor1B(), new ConcreteImplementor2A());
            ab2.Operation();
            Console.Read();
        }
    }
}