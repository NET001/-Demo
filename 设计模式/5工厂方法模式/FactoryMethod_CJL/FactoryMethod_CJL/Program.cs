using System;

namespace FactoryMethod_CJL
{
    class Program
    {
        static void Main(string[] args)
        {
            Creator creatorb = new ConcreteCreatorB();
            Product product = creatorb.CeateProduct();
            product.Operation();
            
            Creator creatora = new ConcreteCreatorA();
            product = creatora.CeateProduct();
            product.Operation();
            Console.ReadKey();
        }
    }
}
