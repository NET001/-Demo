using System;

namespace Bridge_CJL
{
    class Program
    {
        static void Main(string[] args)
        {
            Abstraction ab = new RefinedAbstraction();
            ab.SetImplementor(new ConcreteImplementorA());
            ab.Operation();
            ab.SetImplementor(new ConcreteImplementorB());
            ab.Operation();
            Console.Read();
        }
    }
}