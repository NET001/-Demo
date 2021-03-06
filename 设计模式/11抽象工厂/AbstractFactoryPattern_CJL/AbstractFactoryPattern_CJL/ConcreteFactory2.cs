using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractFactoryPattern_CJL
{
    /// <summary>
    /// 具体工厂2
    /// </summary>
    public class ConcreteFactory2 : AbstractFactory
    {
        public override AbstractProductA CreateProductA()
        {
            return new ProductA2();
        }
        public override AbstractProductB CreateProductB()
        {
            return new ProductB2();
        }
    }
}
