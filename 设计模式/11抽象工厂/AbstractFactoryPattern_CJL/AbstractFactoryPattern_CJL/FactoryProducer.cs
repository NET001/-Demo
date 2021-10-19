using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractFactoryPattern_CJL
{
    /// <summary>
    /// 抽象工厂生成类
    /// </summary>
   public class FactoryProducer
    {
        private AbstractProductA AbstractProductA;
        private AbstractProductB AbstractProductB;

        // Constructor 
        public FactoryProducer(AbstractFactory factory)
        {
            AbstractProductB = factory.CreateProductB();
            AbstractProductA = factory.CreateProductA();
        }

        public void Run()
        {
            AbstractProductB.Interact(AbstractProductA);
        }
    }
}
